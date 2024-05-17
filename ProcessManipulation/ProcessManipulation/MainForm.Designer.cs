namespace ProcessManipulation
{
	partial class MainForm
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
			this.listBoxAvailableAsseblies = new System.Windows.Forms.ListBox();
			this.listBoxStartedAssemblies = new System.Windows.Forms.ListBox();
			this.buttonStart = new System.Windows.Forms.Button();
			this.labelAvailableAssemblues = new System.Windows.Forms.Label();
			this.labelStartedAssemblies = new System.Windows.Forms.Label();
			this.buttonStop = new System.Windows.Forms.Button();
			this.buttonCloseWindow = new System.Windows.Forms.Button();
			this.buttonRefresh = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBoxAvailableAsseblies
			// 
			this.listBoxAvailableAsseblies.FormattingEnabled = true;
			this.listBoxAvailableAsseblies.Location = new System.Drawing.Point(12, 37);
			this.listBoxAvailableAsseblies.Name = "listBoxAvailableAsseblies";
			this.listBoxAvailableAsseblies.Size = new System.Drawing.Size(293, 225);
			this.listBoxAvailableAsseblies.TabIndex = 0;
			// 
			// listBoxStartedAssemblies
			// 
			this.listBoxStartedAssemblies.FormattingEnabled = true;
			this.listBoxStartedAssemblies.Location = new System.Drawing.Point(392, 37);
			this.listBoxStartedAssemblies.Name = "listBoxStartedAssemblies";
			this.listBoxStartedAssemblies.Size = new System.Drawing.Size(293, 225);
			this.listBoxStartedAssemblies.TabIndex = 1;
			// 
			// buttonStart
			// 
			this.buttonStart.Location = new System.Drawing.Point(311, 37);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(75, 23);
			this.buttonStart.TabIndex = 2;
			this.buttonStart.Text = "Start";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// labelAvailableAssemblues
			// 
			this.labelAvailableAssemblues.AutoSize = true;
			this.labelAvailableAssemblues.Location = new System.Drawing.Point(13, 13);
			this.labelAvailableAssemblues.Name = "labelAvailableAssemblues";
			this.labelAvailableAssemblues.Size = new System.Drawing.Size(106, 13);
			this.labelAvailableAssemblues.TabIndex = 3;
			this.labelAvailableAssemblues.Text = "Доступные сборки:";
			// 
			// labelStartedAssemblies
			// 
			this.labelStartedAssemblies.AutoSize = true;
			this.labelStartedAssemblies.Location = new System.Drawing.Point(392, 18);
			this.labelStartedAssemblies.Name = "labelStartedAssemblies";
			this.labelStartedAssemblies.Size = new System.Drawing.Size(114, 13);
			this.labelStartedAssemblies.TabIndex = 4;
			this.labelStartedAssemblies.Text = "Запущенные сборки:";
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(312, 67);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(75, 23);
			this.buttonStop.TabIndex = 5;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// buttonCloseWindow
			// 
			this.buttonCloseWindow.Location = new System.Drawing.Point(312, 97);
			this.buttonCloseWindow.Name = "buttonCloseWindow";
			this.buttonCloseWindow.Size = new System.Drawing.Size(75, 23);
			this.buttonCloseWindow.TabIndex = 6;
			this.buttonCloseWindow.Text = "CloseWindow";
			this.buttonCloseWindow.UseVisualStyleBackColor = true;
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.Location = new System.Drawing.Point(312, 127);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
			this.buttonRefresh.TabIndex = 7;
			this.buttonRefresh.Text = "Refresh";
			this.buttonRefresh.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(698, 360);
			this.Controls.Add(this.buttonRefresh);
			this.Controls.Add(this.buttonCloseWindow);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.labelStartedAssemblies);
			this.Controls.Add(this.labelAvailableAssemblues);
			this.Controls.Add(this.buttonStart);
			this.Controls.Add(this.listBoxStartedAssemblies);
			this.Controls.Add(this.listBoxAvailableAsseblies);
			this.Name = "MainForm";
			this.Text = "Manipulator";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxAvailableAsseblies;
		private System.Windows.Forms.ListBox listBoxStartedAssemblies;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Label labelAvailableAssemblues;
		private System.Windows.Forms.Label labelStartedAssemblies;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonCloseWindow;
		private System.Windows.Forms.Button buttonRefresh;
	}
}

