using AbysmalCore.Debugging;
using AbysmalCore.Extensibility;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace vsmini
{
    public partial class Main : Form
    {
        enum Compiler { csc, ace }
        static Compiler currentCompiler = Compiler.ace;
        static string currentFile = "";

        public Main() => InitializeComponent();

        private void switchCompiler_Click(object sender, EventArgs e)
        {
            if (currentCompiler == Compiler.csc) currentCompiler = Compiler.ace;
            else currentCompiler = Compiler.csc;

            switchCompiler.Text = $"compiler: {currentCompiler}";
        }

        private void Main_Load(object sender, EventArgs e)
        {
            AbysmalDebug.Enabled = false;

            save.Enabled = false;
            run.Enabled = false;
            delAsm.Enabled = false;
            addAsm.Enabled = false;

            Dictionary<Control, string> tooltips = new()
            {
                { open, "loads a file to the editor" },
                { save, "saves the current file" },
                { saveAs, "saves as a new file" },

                { switchCompiler, "switches the compiler between\ncsc and ace\n" +
                "csc - cs compiler: up to c# 5, error verbosity\n" +
                "ace - abysmalcore extensibility: modern, in-mem, more features" },

                { entryMethod, "defines the entrypoint of the app (ace only)" },
                { argsBox, "command line args to pass" },

                { run, "compiles and runs the code" },
                { autoAsm, "adds assembly references from the source" },
                { addAsm, "adds an assembly reference" },
                { asmName, "assembly reference to add" },
                { delAsm, "removes the currently selected assembly reference" },
                { this, "vsmini made by harry" },
            };

            foreach (KeyValuePair<Control, string> kvp in tooltips)
            {
                ToolTip tip = new()
                {
                    UseAnimation = true,
                    UseFading = true
                };
                tip.SetToolTip(kvp.Key, kvp.Value);
            }

            autoAsm_Click(this, null);
        }

        private void addAsm_Click(object sender, EventArgs e) => asmList.Items.Add(asmName.Text);
        private void delAsm_Click(object sender, EventArgs e)
        {
            asmList.Items.RemoveAt(asmList.SelectedIndex);
            delAsm.Enabled = false;
        }
        private void asmList_SelectedIndexChanged(object sender, EventArgs e) => delAsm.Enabled = true;

        private void asmName_TextChanged(object sender, EventArgs e)
        {
            if (asmName.Text != string.Empty)
                addAsm.Enabled = true;
        }

        private void autoAsm_Click(object sender, EventArgs e)
        {
            List<string> duplicates = new();
            List<string> unique = new();

            foreach (string line in code.Lines)
            {
                if (line.Split(' ')[0] == "using")
                {
                    int libIdx = 1;
                    if (line.Split(' ')[1] == "static") libIdx = 2;

                    string newLib = line.Split(" ")[libIdx].Replace(";", "") + ".dll";
                    if (asmList.Items.Contains(newLib)) duplicates.Add(newLib);
                    else unique.Add(newLib);
                }
            }

            asmList.Items.AddRange(unique.ToArray());
            StringBuilder msg = new("[auto assembly] ");

            if (unique.Count > 0)
            {
                msg.AppendLine($"included {unique.Count} new libs:");
                foreach (string x in unique) msg.AppendLine(x);
                msg.AppendLine();
            }

            if (duplicates.Count > 0)
            {
                msg.AppendLine($"found {duplicates.Count} duplicates:");
                foreach (string x in duplicates) msg.AppendLine(x);
            }

            diagnostics.Text = msg.ToString();
        }

        private void open_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new()
            {
                Filter = "C# Files|*.cs|Any File|*.*",
                Title = "Save File"
            };
            dlg.ShowDialog();

            if (dlg.FileName == "") return;

            code.Text = File.ReadAllText(dlg.FileName);
            currentFile = dlg.FileName;
            autoAsm_Click(null, new());

            run.Enabled = true;
            Text = "vsmini";

            loadOptions();
        }

        private void save_Click(object sender, EventArgs e)
        {
            File.WriteAllText(currentFile, code.Text);
            Text = "vsmini";

            save.Enabled = false;
            run.Enabled = true;
        }

        private void code_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            loadOptions();
            if (currentFile == "") Text = "vsmini*";
            else
            {
                if (code.Text == File.ReadAllText(currentFile))
                {
                    save.Enabled = false;
                    run.Enabled = true;
                    Text = "vsmini";
                }
                else
                {
                    save.Enabled = true;
                    run.Enabled = false;
                    Text = "vsmini*";
                }
            }
        }

        private void saveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new()
            {
                Filter = "C# Files|*.cs|Any File|*.*",
                Title = "Save File"
            };
            dlg.ShowDialog();

            if (dlg.FileName == "") return;

            File.WriteAllText(dlg.FileName, code.Text);
            currentFile = dlg.FileName;

            save.Enabled = false;
            run.Enabled = true;
            Text = "vsmini";
        }

        private void loadOptions()
        {
            if (!code.Text.StartsWith("//")) return;

            string[] args = string.Concat(code.Text.Split("\n")[0].Skip(2)).Split(',');
            foreach (string arg in args)
            {
                string k = arg.Split(':')[0];
                string v = arg.Split(':')[1].Trim();

                if (k == "c")
                {
                    if (v == "csc") currentCompiler = Compiler.csc;
                    else currentCompiler = Compiler.ace;

                    switchCompiler_Click(this, null);
                    switchCompiler_Click(this, null);
                }
                else if (k == "ep") entryMethod.Text = v;
                else if (k == "args") argsBox.Text = v;
                else if (k == "argd")
                {
                    Clipboard.SetText(v);
                    if (v == "null") delimiter.Text = string.Empty;
                    else delimiter.Text = v[0].ToString();
                }
            }
        }

        private void run_Click(object sender, EventArgs e)
        {
            loadOptions();
            if (currentCompiler == Compiler.csc)
            {
                save_Click(null, new());
                StringBuilder args = new();

                foreach (string dll in asmList.Items) args.Append($"/r:{dll} ");
                args.Append($"/r:{Path.GetFullPath(".\\AbysmalCore.dll")} ");
                string exePath = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileNameWithoutExtension(currentFile)) + ".exe";
                args.Append($"/out:{exePath} ");
                args.Append(currentFile);

                diagnostics.Text = "[csc] ";
                Process compProc = Process.Start(new ProcessStartInfo()
                {
                    FileName = "C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\csc.exe",
                    Arguments = args.ToString(),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                })!;
                compProc.WaitForExit();

                diagnostics.Text += compProc.StandardOutput.ReadToEnd();

                if (!diagnostics.Text.Contains("error"))
                {
                    diagnostics.Text += "[csc] compiled\n\n";

                    string cmdArgs;
                    if (delimiter.Text == string.Empty) cmdArgs = argsBox.Text;
                    else cmdArgs = argsBox.Text.Replace(delimiter.Text[0], ' ');
                    Process appProc = Process.Start(new ProcessStartInfo()
                    {
                        FileName = exePath,
                        Arguments = cmdArgs,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    })!;
                    appProc.WaitForExit();

                    diagnostics.Text += appProc.StandardOutput.ReadToEnd();
                }
            }
            else
            {
                save_Click(null, new());

                string[] output;
                diagnostics.Text = "[ace] compilation started\n";
                Assembly? asm = ExtensibilityHelper.CompileAssemblyFromString(code.Text, out output);
                diagnostics.Text += string.Join("\n", output
                    .Where(s => !s.Contains("[Hidden]"))
                    .Select(s => $"[ace:diag]{s}"));

                if (asm != null)
                {
                    diagnostics.Text += $"\n[ace] compiled (with {output.Length} diagnostics)\n";
                    UniformAssembly u = ExtensibilityHelper.LoadAssembly(asm, true);

                    TextWriter oOut = Console.Out;
                    diagnostics.Text += $"[ace] invoking entrypoint {entryMethod.Text}\n\n";
                    StringWriter nOut = new StringWriter();
                    Console.SetOut(nOut);

                    string cls = string.Join('.', entryMethod.Text.Split('.').Take(entryMethod.Text.Split('.').Length - 1));
                    string method = entryMethod.Text.Split('.').Last();
                    UniformMethod m = u.Classes[cls].Methods[method];

                    List<string> args = [];
                    if (delimiter.Text == string.Empty)
                        foreach (char c in argsBox.Text.ToCharArray()) 
                            args.Add(c.ToString());
                    else args = argsBox.Text.Split(delimiter.Text[0]).ToList();
                    try
                    {
                        m.Invoke([args.ToArray()]);
                    }
                    catch (Exception ex)
                    {
                        diagnostics.Text += $"\n{ex.GetType().Name}: {ex.Message}";
                    }

                    Console.SetOut(oOut);
                    diagnostics.Text += nOut.ToString();
                }
            }
        }
    }
}
