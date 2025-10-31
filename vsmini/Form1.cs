using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vsmini
{
    public partial class Form1 : Form
    {
        string currentFilePath = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            save.Enabled = false;
            compile.Enabled = false;
            run.Enabled = false;

            auto_Click(null, new());

            docMapCont.Controls.Add(new FastColoredTextBoxNS.DocumentMap()
            {
                Dock = DockStyle.Fill,
                Target = code
            });

            File.Delete(".\\_repl.ln");

            Dictionary<Control, string> tooltips = new()
            {
                { load, "loads a file to the editor" },
                { save, "saves the current file" },
                { saveAs, "saves as a new file" },
                { clear, "clears the editor" },

                { clr, "restarts the evaluator" },
                { repl, "expression to evaluate upon pressing enter" },

                { run, "compiles and runs the code" },
                { runArgs, "command line args to pass" },
                { compile, "starts the compiler" },
                { auto, "adds assembly references from the source" },
                { include, "adds an assembly reference" },
                { lib, "assembly reference to add" },
                { uninclude, "removes the currently selected assembly reference" },
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
        }

        private void lib_TextChanged(object sender, EventArgs e) => include.Enabled = lib.Text != "";
        private void libs_SelectedIndexChanged(object sender, EventArgs e) => uninclude.Enabled = libs.SelectedIndex != -1;
        private void uninclude_Click(object sender, EventArgs e) => libs.Items.RemoveAt(libs.SelectedIndex);
        private void include_Click(object sender, EventArgs e) => libs.Items.Add(lib.Text);

        private void auto_Click(object sender, EventArgs e)
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
                    if (libs.Items.Contains(newLib)) duplicates.Add(newLib);
                    else unique.Add(newLib);
                }
            }

            libs.Items.AddRange(unique.ToArray());
            StringBuilder msg = new();

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

            // MessageBox.Show(msg.ToString(), "vsmini - autoinclude");
        }

        private void load_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new()
            {
                Filter = "C# Files|*.cs|Any File|*.*",
                Title = "Save File"
            };
            dlg.ShowDialog();

            if (dlg.FileName == "") return;

            code.Text = File.ReadAllText(dlg.FileName);
            currentFilePath = dlg.FileName;
            auto_Click(null, new());

            compile.Enabled = true;
            run.Enabled = true;
            Text = "vsmini";
        }

        private void save_Click(object sender, EventArgs e)
        {
            File.WriteAllText(currentFilePath, code.Text);
            Text = "vsmini";

            save.Enabled = false;
            compile.Enabled = true;
            run.Enabled = true;
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
            currentFilePath = dlg.FileName;

            save.Enabled = false;
            compile.Enabled = true;
            run.Enabled = true;
            Text = "vsmini";
        }

        private List<string> compileCode()
        {
            save_Click(null, new());
            StringBuilder args = new();

            foreach (string dll in libs.Items) args.Append($"/r:{dll} ");
            args.Append($"/out:{Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileNameWithoutExtension(currentFilePath))}.exe ");
            args.Append(currentFilePath);

            Process proc = Process.Start(new ProcessStartInfo()
            {
                FileName = "C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\csc.exe",
                Arguments = args.ToString(),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            });
            proc.WaitForExit();

            List<string> errors = new();
            string[] output = proc.StandardOutput.ReadToEnd().Split('\n');
            foreach (string line in output) if (line.Contains("error")) errors.Add(line);

            return errors;
        }

        private void fixErrors(List<string> errors)
        {
            List<string> missingLibs = new();
            List<string> missingChars = new();

            foreach (string err in errors)
            {
                if (err.Contains("CS0006")) missingLibs.Add(err.Split(' ')[4].Replace("'", ""));
                else if (err.Contains("CS1002")) missingChars.Add(err);
            }

            if (missingLibs.Count > 0)
            {
                StringBuilder msg = new("would you like to automatically uninclude the following missing libraries?\n");
                foreach (string err in missingLibs) msg.AppendLine(err);

                if (MessageBox.Show(msg.ToString(), $"{Text} - compiler", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    foreach (string missingLib in missingLibs) libs.Items.Remove(missingLib);
            }

            if (missingChars.Count > 0)
            {
                StringBuilder msg = new("would you like to automatically add missing characters from these errors?\n");
                foreach (string missingChar in missingChars)
                    msg.AppendLine(missingChar);

                if (MessageBox.Show(msg.ToString(), $"{Text} - compiler", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (string missingChar in missingChars)
                    {
                        code.Text = CodeFixer.InsertExpectedChar(missingChar, code.Text);
                    }
                }
            }

            save_Click(null, new());
        }

        private void compile_Click(object sender, EventArgs e)
        {
            List<string> errors = compileCode();

            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), $"{Text} - compiler");
                fixErrors(errors);
            }
            else MessageBox.Show($"compiled to {Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileNameWithoutExtension(currentFilePath))}.exe", $"{Text} - compiler");
        }

        private void run_Click(object sender, EventArgs e)
        {
            List<string> errors = compileCode();

            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), $"{Text} - compiler");
                fixErrors(errors);
            }
            else
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = "C:\\Windows\\System32\\cmd.exe",
                    Arguments = $"/k {Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileNameWithoutExtension(currentFilePath))}.exe {runArgs.Text}",
                    CreateNoWindow = false,
                    UseShellExecute = false
                });
            }
        }

        private void code_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (currentFilePath == "") Text = "vsmini*";
            else
            {
                if (code.Text == File.ReadAllText(currentFilePath))
                {
                    save.Enabled = false;
                    compile.Enabled = true;
                    run.Enabled = true;
                    Text = "vsmini";
                }
                else
                {
                    save.Enabled = true;
                    compile.Enabled = false;
                    run.Enabled = false;
                    Text = "vsmini*";
                }
            }
        }

        private void repl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && repl.Text != "")
            {
                string lnFile;
                try { lnFile = File.ReadAllText(".\\_repl.ln"); }
                catch { lnFile = ""; }

                File.WriteAllText(".\\_repl.ln", $"{lnFile}\n{repl.Text}");
                string pre = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _repl
{
internal class _repl
{
static void Main(string[] args)
{";

                string post = "}\n}\n}";

                File.WriteAllText(".\\_repl.cs", $"{pre}{File.ReadAllText(".\\_repl.ln")}{post}");

                StringBuilder args = new();

                foreach (string dll in libs.Items) args.Append($"/r:{dll} ");
                args.Append($"/out:{Path.Combine(Directory.GetCurrentDirectory(), "_repl.exe")} ");
                args.Append(Path.Combine(Directory.GetCurrentDirectory(), "_repl.cs"));

                Process proc = Process.Start(new ProcessStartInfo()
                {
                    FileName = "C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\csc.exe",
                    Arguments = args.ToString(),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                });
                proc.WaitForExit();

                List<string> errors = new();
                string[] output = proc.StandardOutput.ReadToEnd().Split('\n');
                foreach (string line in output) if (line.Contains("error")) errors.Add(line);

                if (errors.Count > 0)
                {
                    MessageBox.Show(string.Join("\n", errors), $"{Text} - repl");
                    List<string> lines = File.ReadAllLines(".\\_repl.ln").ToList();
                    lines.RemoveAt(lines.Count - 1);
                    File.WriteAllText(".\\_repl.ln", string.Join("\n", lines));
                }
                else
                {
                    Process replProc = Process.Start(new ProcessStartInfo()
                    {
                        FileName = Path.Combine(Directory.GetCurrentDirectory(), "_repl.exe"),
                        Arguments = runArgs.Text,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    });
                    replProc.WaitForExit();
                    string[] result = replProc.StandardOutput.ReadToEnd().Split('\n');

                    int x = 1;
                    if (result.Length > 1) x = 2;

                    if (string.IsNullOrEmpty(result[result.Length - x]) == false) MessageBox.Show(result[result.Length - x], $"{Text} - repl");
                }

                File.Delete(".\\_repl.cs");
                File.Delete(".\\_repl.exe");
            }
        }

        private void clr_Click(object sender, EventArgs e) => File.WriteAllText(".\\_repl.ln", "");
        private void clear_Click(object sender, EventArgs e) => code.Text = "";

        private void auto_MouseEnter(object sender, EventArgs e)
        {
            
        }
    }
}
