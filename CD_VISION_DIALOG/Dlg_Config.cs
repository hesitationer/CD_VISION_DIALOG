﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using CodeKing.Native;
using DEF_PARAMS;


namespace CD_VISION_DIALOG
{
    public partial class Dlg_Config : Form
    {
        PARAM_CONFIG config = new PARAM_CONFIG();

        public delegate void dele_ChangeConfig(PARAM_CONFIG config);
        public event dele_ChangeConfig eventDele_ChangeConfig;

        public Dlg_Config()
        {
            InitializeComponent();
        }

        private void Dlg_Config_Load(object sender, EventArgs e)
        {
            TXT_PATH_i01_MAIN.Text = config.i01_PATH_MAIN;
            TXT_PATH_i02_DATA_DUMP.Text = config.i02_PATH_DATA_DUMP;
            TXT_PATH_i03_RECP_BASE.Text = config.i03_PATH_RECP_BASE;
            TXT_PATH_i04_RECP_REAL.Text = config.i04_PATH_RECP_REAL;

            TXT_PATH_i10_IMAGE_ORG.Text = config.i10_PATH_IMG_ORG;
            TXT_PATH_i11_IMAGE_PTRN.Text = config.i11_PATH_IMG_PTRN;

            TXT_PATH_i15_HISTORY_MEASURE.Text = config.i15_PATH_HIST_MEASURE;
            TXT_PATH_i16_HISTORY_PTRN.Text = config.i16_PATH_HIST_PTRN;

            TXT_PATH_i21_LOG.Text = config.i21_PATH_LOG;
            TXT_PATH_i20_INI.Text = config.i20_PATH_INI;
                
        }
        public bool SetParam(PARAM_CONFIG config)
        {
            this.config = config;

            return true;
        }

        
        public string SelectFolder()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();
            return folderBrowser.SelectedPath;

        }
        private void BTN_SET_PATH_i01_MAIN_Click(object sender, EventArgs e){TXT_PATH_i01_MAIN.Text = SelectFolder();}
        private void BTN_SET_PATH_i02_DATA_DUMP_Click(object sender, EventArgs e){TXT_PATH_i02_DATA_DUMP.Text = SelectFolder();}
        private void BTN_SET_PATH_i03_BASE_RECP_Click(object sender, EventArgs e){TXT_PATH_i03_RECP_BASE.Text = SelectFolder();}
        private void BTN_SET_PATH_i04_REAL_RECP_Click(object sender, EventArgs e){TXT_PATH_i04_RECP_REAL.Text = SelectFolder();}

        private void BTN_SET_PATH_i10_IMAGE_ORG_Click(object sender, EventArgs e){TXT_PATH_i10_IMAGE_ORG.Text = SelectFolder();}
        private void BTN_SET_PATH_i11_IMAGE_PTRN_Click(object sender, EventArgs e){TXT_PATH_i11_IMAGE_PTRN.Text = SelectFolder();}

        private void BTN_SET_PATH_i15_HISTORY_MEASURE_Click(object sender, EventArgs e){TXT_PATH_i15_HISTORY_MEASURE.Text = SelectFolder();        }
        private void BTN_SET_PATH_i16_HISTORY_MATCHING_Click(object sender, EventArgs e){TXT_PATH_i16_HISTORY_PTRN.Text = SelectFolder();}

        private void BTN_SET_PATH_i20_INI_Click(object sender, EventArgs e){TXT_PATH_i20_INI.Text = SelectFolder();}
        private void BTN_SET_PATH_i21_LOG_Click(object sender, EventArgs e) { TXT_PATH_i21_LOG.Text = SelectFolder(); }

        private void BTN_CANCEL_Click(object sender, EventArgs e){this.Hide();}

        private void BTN_APPLY_Click(object sender, EventArgs e)
        {
            PARAM_CONFIG buff = new PARAM_CONFIG();

            buff.i01_PATH_MAIN = TXT_PATH_i01_MAIN.Text;
            buff.i02_PATH_DATA_DUMP = TXT_PATH_i02_DATA_DUMP.Text;
            buff.i03_PATH_RECP_BASE = TXT_PATH_i03_RECP_BASE.Text;
            buff.i04_PATH_RECP_REAL = TXT_PATH_i04_RECP_REAL.Text;

            buff.i10_PATH_IMG_ORG = TXT_PATH_i10_IMAGE_ORG.Text;
            buff.i11_PATH_IMG_PTRN = TXT_PATH_i11_IMAGE_PTRN.Text;

            buff.i15_PATH_HIST_MEASURE = TXT_PATH_i15_HISTORY_MEASURE.Text;
            buff.i16_PATH_HIST_PTRN = TXT_PATH_i16_HISTORY_PTRN.Text;

            buff.i20_PATH_INI = TXT_PATH_i20_INI.Text;
            buff.i21_PATH_LOG = TXT_PATH_i21_LOG.Text;

            eventDele_ChangeConfig(buff);
            this.Hide();
        }

        #region glass effect
        // defines how far we are extending the Glass margins
        private Win32.MARGINS margins;
        private int padding_TOP = 5;
        private int padding_LFT = 5;
        private int padding_RHT = 5;
        private int padding_BTM = 5;
        /// <summary>
        /// Override the onload, and define our Glass margins
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Win32.DwmIsCompositionEnabled())
            {
                MessageBox.Show("This demo requires Vista, with Aero enabled.");
                Application.Exit();
            }
            SetGlassRegion();
        }
        /// <summary>
        /// Use the form padding values to define a Glass margin
        /// </summary>
        private void SetGlassRegion()
        {
            // Set up the glass effect using padding as the defining glass region
            if (Win32.DwmIsCompositionEnabled())
            {
                margins = new Win32.MARGINS();
                margins.Top = padding_TOP;
                margins.Left = padding_LFT;
                margins.Bottom = padding_BTM;
                margins.Right = padding_RHT;
                Win32.DwmExtendFrameIntoClientArea(this.Handle, ref margins);
            }
        }
        /// <summary>
        /// Override the OnPaintBackground method, to draw the desired
        /// Glass regions black and display as Glass
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Win32.DwmIsCompositionEnabled())
            {
                e.Graphics.Clear(Color.Black);
                // put back the original form background for non-glass area
                Rectangle clientArea = new Rectangle(
                margins.Left,
                margins.Top,
                this.ClientRectangle.Width - margins.Left - margins.Right,
                this.ClientRectangle.Height - margins.Top - margins.Bottom);
                Brush b = new SolidBrush(this.BackColor);
                e.Graphics.FillRectangle(b, clientArea);
            }
        }

        #endregion

        private void BTN_OPEN_PATH_i01_Click(object sender, EventArgs e) { Process.Start(config.i01_PATH_MAIN); }
        private void BTN_OPEN_PATH_i02_Click(object sender, EventArgs e) { Process.Start(config.i02_PATH_DATA_DUMP); }
        private void BTN_OPEN_PATH_i03_Click(object sender, EventArgs e) { Process.Start(config.i03_PATH_RECP_BASE); }
        private void BTN_OPEN_PATH_i04_Click(object sender, EventArgs e) { Process.Start(config.i04_PATH_RECP_REAL); }
        private void BTN_OPEN_PATH_i11_Click(object sender, EventArgs e) { Process.Start(config.i11_PATH_IMG_PTRN); }

        private void BTN_OPEN_PATH_i10_Click(object sender, EventArgs e) { Process.Start(config.i10_PATH_IMG_ORG); }
        private void BTN_OPEN_PATH_i15_Click(object sender, EventArgs e) { Process.Start(config.i15_PATH_HIST_MEASURE); }
        private void BTN_OPEN_PATH_i16_Click(object sender, EventArgs e) { Process.Start(config.i16_PATH_HIST_PTRN); }
        private void BTN_OPEN_PATH_i20_Click(object sender, EventArgs e) { Process.Start(config.i20_PATH_INI); }
        private void BTN_OPEN_PATH_i21_Click(object sender, EventArgs e) { Process.Start(config.i21_PATH_LOG); }

    }
}