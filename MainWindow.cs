using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WearDataHelper
{
    public partial class MainWindow : Form
    {
        private List<PumpAttributes> listAttributes = new List<PumpAttributes>();
        private List<PictureBox> listPictureBoxes = new List<PictureBox>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            listAttributes.Add(new PumpAttributes() { PartName = "VOLUTE LINER FRONT" });
            listAttributes.Add(new PumpAttributes() { PartName = "VOLUTE LINER CUTWATER" });
            listAttributes.Add(new PumpAttributes() { PartName = "IMPELLER FRONT" });
            listAttributes.Add(new PumpAttributes() { PartName = "IMPELLER SIDE" });
            listAttributes.Add(new PumpAttributes() { PartName = "THROATBUSH" });
            listAttributes.Add(new PumpAttributes() { PartName = "FRAME PLATE LINER INSERT" });

            int xPos = 0, yPos = 72, xOffset = 0, count = 0;
            foreach (var attrib in listAttributes)
            {
                if (xOffset > 400)
                {
                    xPos = 0;
                    yPos = 240;
                }

                GroupBox gbPart = new GroupBox() { Text = attrib.PartName };
                gbPart.Size = new Size(200, 150);

                xOffset = xPos++ * (gbPart.Width + 16);
                gbPart.Location = new Point(8 + xOffset, yPos);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Tag = count++;
                pictureBox.AllowDrop = true;
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.DragEnter += PbPart_DragEnter;
                pictureBox.DragDrop += PbPart_DragDrop;
                pictureBox.Click += pbPart_Click;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                // pictureBox.BackColor = SystemColors.ControlDark;
                pictureBox.Location = new Point(8, 16);

                Bitmap bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    Font font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular, GraphicsUnit.Point);
                    g.Clear(Color.White);
                    g.DrawString("Drag and drop\nphoto here...", font, Brushes.Gray, 0, 0);
                }
                pictureBox.Image = bmp;

                listPictureBoxes.Add(pictureBox);
                gbPart.Controls.Add(pictureBox);
                Controls.Add(gbPart);
            }

            pgAttributes.Location = new Point(8, yPos + 158);

            pgAttributes.Width = xOffset + listPictureBoxes[listPictureBoxes.Count - 1].Width + 8;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void PbPart_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox pbPart = sender as PictureBox;
            var files = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            PumpAttributes attributes = listAttributes[(int)pbPart.Tag];

            attributes.ImageFilePath = files[0];
            if (string.IsNullOrEmpty(txtAssetNum.Text))
            {
                txtAssetNum.Text = Path.GetFileName(Path.GetDirectoryName(files[0]));
            }
            using (var img = Image.FromFile(attributes.ImageFilePath))
            {
                pbPart.Image = new Bitmap(img);
            }
        }

        private void PbPart_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) ||
                e.Data.GetDataPresent(DataFormats.Bitmap, false) ||
                e.Data.GetDataPresent(DataFormats.Text, false))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pbPart_Click(object sender, EventArgs e)
        {
            foreach (PictureBox item in listPictureBoxes)
            {
                item.BorderStyle = BorderStyle.FixedSingle;
            }

            PictureBox pb = sender as PictureBox;
            pb.BorderStyle = BorderStyle.Fixed3D;
            pgAttributes.SelectedObject = listAttributes[(int)pb.Tag];
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            // error checking
            if (string.IsNullOrEmpty(txtAssetNum.Text))
            {
                MessageBox.Show("Asset Number is empty.");
                return;
            }

            if (string.IsNullOrEmpty(txtWorkOrderNum.Text))
            {
                MessageBox.Show("Work Order Number is empty.");
                return;
            }

            foreach (PumpAttributes attrib in listAttributes)
            {
                // 201705 Group 01-1 IMPELLER FRONT.JPG
                if (File.Exists(attrib.ImageFilePath))
                {
                    attrib.DateOverhaul = dtpOH.Value.ToShortDateString();
                    attrib.PartUniqueID = $"{dtpOH.Value.ToString("yyyyMM")} {txtAssetNum.Text.Trim()} {attrib.PartName}";
                    attrib.WorkOrderNumber = txtWorkOrderNum.Text;
                    string fpNew = $"{Path.Combine(Path.GetDirectoryName(attrib.ImageFilePath), attrib.PartUniqueID)}{Path.GetExtension(attrib.ImageFilePath)}";

                    if (File.Exists(fpNew) && !fpNew.Equals(attrib.ImageFilePath, StringComparison.OrdinalIgnoreCase))
                    {
                        // do not delete if the old file name is exactly as new
                        File.Delete(fpNew);
                        File.Move(attrib.ImageFilePath, fpNew);
                    }
                }
            }
        }

        private void btnCreateCsv_Click(object sender, EventArgs e)
        {
            string fpCsv = Path.Combine(Path.GetDirectoryName(listAttributes[0].ImageFilePath), txtAssetNum.Text + ".csv");
            if (File.Exists(fpCsv)) File.Delete(fpCsv);
            using (StreamWriter sw = new StreamWriter(fpCsv))
            {
                CsvSerializer.Serialize<PumpAttributes>(sw, listAttributes);
            }
        }
    }
}