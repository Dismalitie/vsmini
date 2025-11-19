namespace vsmini
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Label entryLabel;
            Label argsLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            code = new FastColoredTextBoxNS.FastColoredTextBox();
            asmList = new ListBox();
            run = new Button();
            save = new Button();
            open = new Button();
            addAsm = new Button();
            autoAsm = new Button();
            delAsm = new Button();
            asmName = new TextBox();
            saveAs = new Button();
            switchCompiler = new Button();
            diagnostics = new RichTextBox();
            entryMethod = new TextBox();
            argsBox = new TextBox();
            delimiter = new TextBox();
            entryLabel = new Label();
            argsLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)code).BeginInit();
            SuspendLayout();
            // 
            // entryLabel
            // 
            entryLabel.AutoSize = true;
            entryLabel.Location = new Point(12, 653);
            entryLabel.Name = "entryLabel";
            entryLabel.Size = new Size(76, 19);
            entryLabel.TabIndex = 15;
            entryLabel.Text = "Entrypoint:";
            // 
            // argsLabel
            // 
            argsLabel.AutoSize = true;
            argsLabel.Location = new Point(278, 653);
            argsLabel.Name = "argsLabel";
            argsLabel.Size = new Size(80, 19);
            argsLabel.TabIndex = 17;
            argsLabel.Text = "Arguments:";
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
            code.AutoScrollMinSize = new Size(542, 272);
            code.BackBrush = null;
            code.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            code.CharHeight = 17;
            code.CharWidth = 9;
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
            code.Size = new Size(942, 631);
            code.TabIndex = 1;
            code.Text = resources.GetString("code.Text");
            code.VirtualSpace = true;
            code.Zoom = 100;
            code.TextChanged += code_TextChanged;
            // 
            // asmList
            // 
            asmList.Font = new Font("Cascadia Code", 9.163636F, FontStyle.Regular, GraphicsUnit.Point, 0);
            asmList.FormattingEnabled = true;
            asmList.ItemHeight = 18;
            asmList.Location = new Point(960, 12);
            asmList.Name = "asmList";
            asmList.Size = new Size(178, 634);
            asmList.TabIndex = 3;
            asmList.SelectedIndexChanged += asmList_SelectedIndexChanged;
            // 
            // run
            // 
            run.Location = new Point(960, 713);
            run.Name = "run";
            run.Size = new Size(86, 44);
            run.TabIndex = 5;
            run.Text = "run";
            run.UseVisualStyleBackColor = true;
            run.Click += run_Click;
            // 
            // save
            // 
            save.Location = new Point(1052, 763);
            save.Name = "save";
            save.Size = new Size(86, 26);
            save.TabIndex = 6;
            save.Text = "save";
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // open
            // 
            open.Location = new Point(960, 763);
            open.Name = "open";
            open.Size = new Size(86, 26);
            open.TabIndex = 7;
            open.Text = "open";
            open.UseVisualStyleBackColor = true;
            open.Click += open_Click;
            // 
            // addAsm
            // 
            addAsm.Location = new Point(960, 649);
            addAsm.Name = "addAsm";
            addAsm.Size = new Size(52, 26);
            addAsm.TabIndex = 8;
            addAsm.Text = "+";
            addAsm.UseVisualStyleBackColor = true;
            addAsm.Click += addAsm_Click;
            // 
            // autoAsm
            // 
            autoAsm.Location = new Point(1018, 648);
            autoAsm.Name = "autoAsm";
            autoAsm.Size = new Size(62, 26);
            autoAsm.TabIndex = 9;
            autoAsm.Text = "auto";
            autoAsm.UseVisualStyleBackColor = true;
            autoAsm.Click += autoAsm_Click;
            // 
            // delAsm
            // 
            delAsm.Location = new Point(1086, 649);
            delAsm.Name = "delAsm";
            delAsm.Size = new Size(52, 26);
            delAsm.TabIndex = 10;
            delAsm.Text = "-";
            delAsm.UseVisualStyleBackColor = true;
            delAsm.Click += delAsm_Click;
            // 
            // asmName
            // 
            asmName.Location = new Point(960, 681);
            asmName.Name = "asmName";
            asmName.Size = new Size(178, 26);
            asmName.TabIndex = 11;
            asmName.TextChanged += asmName_TextChanged;
            // 
            // saveAs
            // 
            saveAs.Location = new Point(960, 795);
            saveAs.Name = "saveAs";
            saveAs.Size = new Size(178, 26);
            saveAs.TabIndex = 12;
            saveAs.Text = "save as";
            saveAs.UseVisualStyleBackColor = true;
            saveAs.Click += saveAs_Click;
            // 
            // switchCompiler
            // 
            switchCompiler.Font = new Font("Segoe UI", 8F);
            switchCompiler.Location = new Point(1052, 713);
            switchCompiler.Name = "switchCompiler";
            switchCompiler.Size = new Size(86, 44);
            switchCompiler.TabIndex = 13;
            switchCompiler.Text = "compiler: ace";
            switchCompiler.UseVisualStyleBackColor = true;
            switchCompiler.Click += switchCompiler_Click;
            // 
            // diagnostics
            // 
            diagnostics.AcceptsTab = true;
            diagnostics.AutoWordSelection = true;
            diagnostics.Font = new Font("Cascadia Code", 9.163636F, FontStyle.Regular, GraphicsUnit.Point, 0);
            diagnostics.Location = new Point(12, 681);
            diagnostics.Name = "diagnostics";
            diagnostics.ReadOnly = true;
            diagnostics.Size = new Size(942, 140);
            diagnostics.TabIndex = 14;
            diagnostics.Text = "";
            // 
            // entryMethod
            // 
            entryMethod.Font = new Font("Cascadia Code", 9.163636F, FontStyle.Regular, GraphicsUnit.Point, 0);
            entryMethod.Location = new Point(94, 650);
            entryMethod.Name = "entryMethod";
            entryMethod.Size = new Size(178, 24);
            entryMethod.TabIndex = 16;
            entryMethod.Text = "App.App.Main";
            // 
            // argsBox
            // 
            argsBox.Font = new Font("Cascadia Code", 9.163636F, FontStyle.Regular, GraphicsUnit.Point, 0);
            argsBox.Location = new Point(364, 650);
            argsBox.Name = "argsBox";
            argsBox.Size = new Size(178, 24);
            argsBox.TabIndex = 18;
            argsBox.Text = "Hello world!";
            // 
            // delimiter
            // 
            delimiter.Font = new Font("Cascadia Code", 9.163636F, FontStyle.Regular, GraphicsUnit.Point, 0);
            delimiter.Location = new Point(548, 651);
            delimiter.MaxLength = 1;
            delimiter.Name = "delimiter";
            delimiter.Size = new Size(18, 24);
            delimiter.TabIndex = 20;
            delimiter.Text = " ";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1150, 833);
            Controls.Add(delimiter);
            Controls.Add(argsBox);
            Controls.Add(argsLabel);
            Controls.Add(entryMethod);
            Controls.Add(entryLabel);
            Controls.Add(diagnostics);
            Controls.Add(switchCompiler);
            Controls.Add(saveAs);
            Controls.Add(asmName);
            Controls.Add(delAsm);
            Controls.Add(autoAsm);
            Controls.Add(addAsm);
            Controls.Add(open);
            Controls.Add(save);
            Controls.Add(run);
            Controls.Add(asmList);
            Controls.Add(code);
            Name = "Main";
            Text = "vsmini*";
            Load += Main_Load;
            ((System.ComponentModel.ISupportInitialize)code).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox code;
        private ListBox asmList;
        private Button run;
        private Button save;
        private Button open;
        private Button addAsm;
        private Button autoAsm;
        private Button delAsm;
        private TextBox asmName;
        private Button saveAs;
        private Button switchCompiler;
        private RichTextBox diagnostics;
        private Label entryLabel;
        private TextBox entryMethod;
        private TextBox argsBox;
        private TextBox delimiter;
    }
}