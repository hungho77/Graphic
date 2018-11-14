namespace Graph
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.ima_btn = new System.Windows.Forms.ImageList(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.btn_Rectangle = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_TamGiacDeu = new System.Windows.Forms.Button();
            this.btn_NguGiac = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.openGLControl = new SharpGL.OpenGLControl();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btn_BangMau = new System.Windows.Forms.Button();
            this.lbl_Size = new System.Windows.Forms.Label();
            this.cmBox_Size = new System.Windows.Forms.ComboBox();
            this.btn_Select = new System.Windows.Forms.Button();
            this.lbl_msg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.ImageIndex = 0;
            this.button1.ImageList = this.ima_btn;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 46);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_Line);
            // 
            // ima_btn
            // 
            this.ima_btn.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ima_btn.ImageStream")));
            this.ima_btn.TransparentColor = System.Drawing.Color.Transparent;
            this.ima_btn.Images.SetKeyName(0, "Icons8-Ios7-Editing-Line.ico");
            this.ima_btn.Images.SetKeyName(1, "Icons8-Ios7-Editing-Ellipse.ico");
            this.ima_btn.Images.SetKeyName(2, "if_icon-ios7-circle-outline_211717.ico");
            this.ima_btn.Images.SetKeyName(3, "plain-triangle.png");
            this.ima_btn.Images.SetKeyName(4, "pentagon-outline-shape.png");
            this.ima_btn.Images.SetKeyName(5, "polygon.png");
            this.ima_btn.Images.SetKeyName(6, "rectangle.png");
            // 
            // button2
            // 
            this.button2.ImageKey = "if_icon-ios7-circle-outline_211717.ico";
            this.button2.ImageList = this.ima_btn;
            this.button2.Location = new System.Drawing.Point(12, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(54, 54);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btn_Circle);
            // 
            // btn_Rectangle
            // 
            this.btn_Rectangle.ImageIndex = 6;
            this.btn_Rectangle.ImageList = this.ima_btn;
            this.btn_Rectangle.Location = new System.Drawing.Point(12, 124);
            this.btn_Rectangle.Name = "btn_Rectangle";
            this.btn_Rectangle.Size = new System.Drawing.Size(54, 48);
            this.btn_Rectangle.TabIndex = 2;
            this.btn_Rectangle.UseVisualStyleBackColor = true;
            this.btn_Rectangle.Click += new System.EventHandler(this.btn_Rectangle_Click);
            // 
            // button4
            // 
            this.button4.ImageIndex = 1;
            this.button4.ImageList = this.ima_btn;
            this.button4.Location = new System.Drawing.Point(12, 178);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(54, 44);
            this.button4.TabIndex = 3;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_TamGiacDeu
            // 
            this.btn_TamGiacDeu.ImageIndex = 3;
            this.btn_TamGiacDeu.ImageList = this.ima_btn;
            this.btn_TamGiacDeu.Location = new System.Drawing.Point(12, 228);
            this.btn_TamGiacDeu.Name = "btn_TamGiacDeu";
            this.btn_TamGiacDeu.Size = new System.Drawing.Size(54, 47);
            this.btn_TamGiacDeu.TabIndex = 4;
            this.btn_TamGiacDeu.UseVisualStyleBackColor = true;
            this.btn_TamGiacDeu.Click += new System.EventHandler(this.btn_TamGiacDeu_Click);
            // 
            // btn_NguGiac
            // 
            this.btn_NguGiac.ImageIndex = 4;
            this.btn_NguGiac.ImageList = this.ima_btn;
            this.btn_NguGiac.Location = new System.Drawing.Point(12, 281);
            this.btn_NguGiac.Name = "btn_NguGiac";
            this.btn_NguGiac.Size = new System.Drawing.Size(54, 49);
            this.btn_NguGiac.TabIndex = 5;
            this.btn_NguGiac.UseVisualStyleBackColor = true;
            this.btn_NguGiac.Click += new System.EventHandler(this.btn_NguGiac_Click);
            // 
            // button7
            // 
            this.button7.ImageIndex = 5;
            this.button7.ImageList = this.ima_btn;
            this.button7.Location = new System.Drawing.Point(12, 336);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(54, 47);
            this.button7.TabIndex = 6;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // openGLControl
            // 
            this.openGLControl.DrawFPS = false;
            this.openGLControl.Location = new System.Drawing.Point(181, 70);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(546, 313);
            this.openGLControl.TabIndex = 7;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            this.openGLControl.Load += new System.EventHandler(this.openGLControl_Load);
            this.openGLControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseClick);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseDown);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseUp);
            // 
            // btn_BangMau
            // 
            this.btn_BangMau.Location = new System.Drawing.Point(12, 397);
            this.btn_BangMau.Name = "btn_BangMau";
            this.btn_BangMau.Size = new System.Drawing.Size(87, 41);
            this.btn_BangMau.TabIndex = 8;
            this.btn_BangMau.Text = "Bảng Màu";
            this.btn_BangMau.UseVisualStyleBackColor = true;
            this.btn_BangMau.Click += new System.EventHandler(this.btn_BangMau_Click);
            // 
            // lbl_Size
            // 
            this.lbl_Size.AutoSize = true;
            this.lbl_Size.Location = new System.Drawing.Point(112, 15);
            this.lbl_Size.Name = "lbl_Size";
            this.lbl_Size.Size = new System.Drawing.Size(35, 17);
            this.lbl_Size.TabIndex = 10;
            this.lbl_Size.Text = "Size";
            // 
            // cmBox_Size
            // 
            this.cmBox_Size.FormattingEnabled = true;
            this.cmBox_Size.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmBox_Size.Location = new System.Drawing.Point(165, 12);
            this.cmBox_Size.Name = "cmBox_Size";
            this.cmBox_Size.Size = new System.Drawing.Size(121, 24);
            this.cmBox_Size.TabIndex = 11;
            this.cmBox_Size.SelectedIndexChanged += new System.EventHandler(this.cmBox_Size_SelectedIndexChanged);
            // 
            // btn_Select
            // 
            this.btn_Select.Location = new System.Drawing.Point(314, 12);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(63, 43);
            this.btn_Select.TabIndex = 12;
            this.btn_Select.Text = "Select";
            this.btn_Select.UseVisualStyleBackColor = true;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // lbl_msg
            // 
            this.lbl_msg.AutoSize = true;
            this.lbl_msg.Location = new System.Drawing.Point(537, 15);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(62, 17);
            this.lbl_msg.TabIndex = 13;
            this.lbl_msg.Text = "labelllllllll";
            this.lbl_msg.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbl_msg);
            this.Controls.Add(this.btn_Select);
            this.Controls.Add(this.cmBox_Size);
            this.Controls.Add(this.lbl_Size);
            this.Controls.Add(this.btn_BangMau);
            this.Controls.Add(this.openGLControl);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.btn_NguGiac);
            this.Controls.Add(this.btn_TamGiacDeu);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btn_Rectangle);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Paint";
            this.Load += new System.EventHandler(this.Paint_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Paint_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_Rectangle;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btn_TamGiacDeu;
        private System.Windows.Forms.Button btn_NguGiac;
        private System.Windows.Forms.Button button7;
        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btn_BangMau;
        private System.Windows.Forms.Label lbl_Size;
        private System.Windows.Forms.ComboBox cmBox_Size;
        private System.Windows.Forms.ImageList ima_btn;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.Label lbl_msg;
    }
}

