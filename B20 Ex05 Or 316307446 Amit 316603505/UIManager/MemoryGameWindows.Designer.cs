﻿namespace UIManager
{
    internal partial class MemoryGameWindows
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
        private void InitializeComponents()
        {
            this.Text = "NewMemoryGameWindows";

            this.SuspendLayout();
            // 
            // MemoryGame
            // 
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "MemoryGame";
            this.Text = "Memory Game";
            this.ResumeLayout(false);
            this.m_CurrentPlayersTurn = new System.Windows.Forms.Label();
            this.m_FirstPlayerScore = new System.Windows.Forms.Label();
            this.m_SecondPlayerScore = new System.Windows.Forms.Label();
        }
    }
        #endregion
}