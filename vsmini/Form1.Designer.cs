namespace vsmini
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            code = new FastColoredTextBoxNS.FastColoredTextBox();
            compile = new Button();
            save = new Button();
            load = new Button();
            libs = new ListBox();
            lib = new TextBox();
            include = new Button();
            run = new Button();
            uninclude = new Button();
            auto = new Button();
            saveAs = new Button();
            runArgs = new TextBox();
            docMapCont = new Panel();
            repl = new TextBox();
            clr = new Button();
            clear = new Button();
            ((System.ComponentModel.ISupportInitialize)code).BeginInit();
            SuspendLayout();
            // 
            // code
            // 
            code.AutoCompleteBrackets = true;
            code.AutoCompleteBracketsList = new char[]
    {
    '(',
    ')',
    '{',
    '}',
    '[',
    ']',
    '"',
    '"',
    '\'',
    '\''
    };
            code.AutoIndentChars = false;
            code.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            code.AutoScrollMinSize = new Size(511, 285);
            code.BackBrush = null;
            code.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            code.CharHeight = 19;
            code.CharWidth = 10;
            code.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            code.Font = new Font("Cascadia Code", 9.792F);
            code.Hotkeys = resources.GetString("code.Hotkeys");
            code.IsReplaceMode = false;
            code.Language = FastColoredTextBoxNS.Language.CSharp;
            code.LeftBracket = '(';
            code.LeftBracket2 = '{';
            code.Location = new Point(12, 12);
            code.Name = "code";
            code.Paddings = new Padding(0);
            code.RightBracket = ')';
            code.RightBracket2 = '}';
            code.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            code.ServiceColors = (FastColoredTextBoxNS.ServiceColors)resources.GetObject("code.ServiceColors");
            code.Size = new Size(1041, 805);
            code.TabIndex = 0;
            code.Text = resources.GetString("code.Text");
            code.VirtualSpace = true;
            code.Zoom = 100;
            code.TextChanged += code_TextChanged;
            // 
            // compile
            // 
            compile.Image = Properties.Resources.cog;
            compile.Location = new Point(1013, 822);
            compile.Name = "compile";
            compile.Size = new Size(40, 40);
            compile.TabIndex = 1;
            compile.UseVisualStyleBackColor = true;
            compile.Click += compile_Click;
            // 
            // save
            // 
            save.Image = Properties.Resources.save;
            save.Location = new Point(58, 822);
            save.Name = "save";
            save.Size = new Size(40, 40);
            save.TabIndex = 3;
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // load
            // 
            load.Image = Properties.Resources.folder_open;
            load.Location = new Point(12, 822);
            load.Name = "load";
            load.Size = new Size(40, 40);
            load.TabIndex = 4;
            load.UseVisualStyleBackColor = true;
            load.Click += load_Click;
            // 
            // libs
            // 
            libs.FormattingEnabled = true;
            libs.ItemHeight = 21;
            libs.Location = new Point(1059, 432);
            libs.Name = "libs";
            libs.Size = new Size(207, 298);
            libs.TabIndex = 5;
            libs.SelectedIndexChanged += libs_SelectedIndexChanged;
            // 
            // lib
            // 
            lib.Font = new Font("Cascadia Code", 9.216F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lib.Location = new Point(1059, 788);
            lib.Name = "lib";
            lib.Size = new Size(207, 26);
            lib.TabIndex = 6;
            lib.Text = "System.Windows.Forms.dll";
            lib.TextChanged += lib_TextChanged;
            // 
            // include
            // 
            include.Image = Properties.Resources.plus;
            include.Location = new Point(1059, 825);
            include.Name = "include";
            include.Size = new Size(98, 30);
            include.TabIndex = 7;
            include.UseVisualStyleBackColor = true;
            include.Click += include_Click;
            // 
            // run
            // 
            run.Image = Properties.Resources.play;
            run.Location = new Point(967, 822);
            run.Name = "run";
            run.Size = new Size(40, 40);
            run.TabIndex = 8;
            run.UseVisualStyleBackColor = true;
            run.Click += run_Click;
            // 
            // uninclude
            // 
            uninclude.Image = Properties.Resources.minus;
            uninclude.Location = new Point(1168, 825);
            uninclude.Name = "uninclude";
            uninclude.Size = new Size(98, 30);
            uninclude.TabIndex = 9;
            uninclude.UseVisualStyleBackColor = true;
            uninclude.Click += uninclude_Click;
            // 
            // auto
            // 
            auto.Image = Properties.Resources.sparkles;
            auto.Location = new Point(1059, 739);
            auto.Name = "auto";
            auto.Size = new Size(207, 43);
            auto.TabIndex = 10;
            auto.UseVisualStyleBackColor = true;
            auto.Click += auto_Click;
            auto.MouseEnter += auto_MouseEnter;
            // 
            // saveAs
            // 
            saveAs.Image = Properties.Resources.save_all;
            saveAs.Location = new Point(104, 822);
            saveAs.Name = "saveAs";
            saveAs.Size = new Size(40, 40);
            saveAs.TabIndex = 11;
            saveAs.UseVisualStyleBackColor = true;
            saveAs.Click += saveAs_Click;
            // 
            // runArgs
            // 
            runArgs.Font = new Font("Cascadia Code", 9.216F, FontStyle.Regular, GraphicsUnit.Point, 0);
            runArgs.Location = new Point(785, 826);
            runArgs.Name = "runArgs";
            runArgs.Size = new Size(176, 26);
            runArgs.TabIndex = 12;
            runArgs.Text = "arg1 arg2";
            // 
            // docMapCont
            // 
            docMapCont.Location = new Point(1059, 12);
            docMapCont.Name = "docMapCont";
            docMapCont.Size = new Size(207, 414);
            docMapCont.TabIndex = 13;
            // 
            // repl
            // 
            repl.Font = new Font("Cascadia Code", 9.216F, FontStyle.Regular, GraphicsUnit.Point, 0);
            repl.Location = new Point(196, 829);
            repl.Name = "repl";
            repl.Size = new Size(309, 26);
            repl.TabIndex = 14;
            repl.Text = "Console.WriteLine(\"Hello!\");";
            repl.KeyDown += repl_KeyDown;
            // 
            // clr
            // 
            clr.Image = Properties.Resources.rotate_ccw;
            clr.Location = new Point(511, 822);
            clr.Name = "clr";
            clr.Size = new Size(40, 40);
            clr.TabIndex = 15;
            clr.UseVisualStyleBackColor = true;
            clr.Click += clr_Click;
            // 
            // clear
            // 
            clear.Image = Properties.Resources.brush_cleaning;
            clear.Location = new Point(150, 822);
            clear.Name = "clear";
            clear.Size = new Size(40, 40);
            clear.TabIndex = 16;
            clear.UseVisualStyleBackColor = true;
            clear.Click += clear_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1278, 865);
            Controls.Add(clear);
            Controls.Add(clr);
            Controls.Add(repl);
            Controls.Add(docMapCont);
            Controls.Add(lib);
            Controls.Add(auto);
            Controls.Add(include);
            Controls.Add(uninclude);
            Controls.Add(runArgs);
            Controls.Add(saveAs);
            Controls.Add(run);
            Controls.Add(libs);
            Controls.Add(load);
            Controls.Add(save);
            Controls.Add(compile);
            Controls.Add(code);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "vsmini*";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)code).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox code;
        private Button compile;
        private Button options;
        private Button save;
        private Button load;
        private ListBox libs;
        private TextBox lib;
        private Button include;
        private Button run;
        private Button uninclude;
        private Button auto;
        private Button saveAs;
        private TextBox runArgs;
        private Panel docMapCont;
        private TextBox repl;
        private Button clr;
        private Button clear;
    }
}
