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
            listAttributes.Add(new PumpAttributes() { PartUniqueID = "VOLUTE LINER FRONT" });
            listAttributes.Add(new PumpAttributes() { PartUniqueID = "VOLUTE LINER CUTWATER" });
            listAttributes.Add(new PumpAttributes() { PartUniqueID = "IMPELLER FRONT" });
            listAttributes.Add(new PumpAttributes() { PartUniqueID = "IMPELLER SIDE" });
            listAttributes.Add(new PumpAttributes() { PartUniqueID = "THROATBUSH" });
            listAttributes.Add(new PumpAttributes() { PartUniqueID = "FRAME PLATE LINER INSERT" });

            int i = 0, yPos = 72, xOffset = 0;
            foreach (var attrib in listAttributes)
            {
                if (xOffset > 400)
                {
                    i = 0;
                    yPos = 240;
                }

                GroupBox gbPart = new GroupBox() { Text = attrib.PartUniqueID };
                gbPart.Size = new Size(200, 150);

                xOffset = i * (gbPart.Width + 16);
                gbPart.Location = new Point(8 + xOffset, yPos);

                PictureBox pictureBox = new PictureBox();
                pictureBox.Tag = listAttributes[i++];
                pictureBox.AllowDrop = true;
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.DragEnter += PbPart_DragEnter;
                pictureBox.DragDrop += PbPart_DragDrop;
                pictureBox.Click += pbPart_Click;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                pictureBox.BackColor = SystemColors.ControlDark;
                pictureBox.Location = new Point(8, 16);

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
            PumpAttributes attributes = pbPart.Tag as PumpAttributes;

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
            pgAttributes.SelectedObject = pb.Tag as PumpAttributes;
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            // error checking
            if (string.IsNullOrEmpty(txtAssetNum.Text))
            {
                MessageBox.Show("Asset # is empty.");
                return;
            }

            foreach (PictureBox pbPart in listPictureBoxes)
            {
                PumpAttributes attrib = pbPart.Tag as PumpAttributes;

                // 201705 Group 01-1 IMPELLER FRONT.JPG
                if (File.Exists(attrib.ImageFilePath))
                {
                    string fnNew = $"{dtpOH.Value.ToString("yyyyMM")} {txtAssetNum.Text.Trim()} {((PumpAttributes)pbPart.Tag).PartUniqueID}";
                    string fpNew = $"{Path.Combine(Path.GetDirectoryName(attrib.ImageFilePath), fnNew)}{Path.GetExtension(attrib.ImageFilePath)}";

                    File.Move(attrib.ImageFilePath, fpNew);
                }
            }

            string fpCsv = Path.Combine(Path.GetDirectoryName(listAttributes[0].ImageFilePath), txtAssetNum.Text + ".csv");
            using (StreamWriter sw = new StreamWriter(fpCsv))
            {
                CsvSerializer.Serialize<PumpAttributes>(sw, listAttributes);
            }
        }
    }
}