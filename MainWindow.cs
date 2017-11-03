using Microsoft.VisualBasic.FileIO;
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
        private string dirPhotos;
        private string dirPumpGroup;

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

            gbAttributes.Location = new Point(8, yPos + 158);

            gbAttributes.Width = xOffset + listPictureBoxes[listPictureBoxes.Count - 1].Width + 8;
        }

        private void ReadCsv(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                int row = 0, col = 0;
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] columns = parser.ReadFields();
                    foreach (string columnText in columns)
                    {
                        if (row > 0)
                        {
                            if (col == 0)
                                listAttributes[row - 1].PartUniqueID = columnText;
                            if (col == 1)
                                listAttributes[row - 1].WorkOrderNumber = columnText;
                            if (col == 2)
                                listAttributes[row - 1].ResidualLife = columnText;
                            if (col == 3)
                                listAttributes[row - 1].PumpServiceLife = columnText;
                            if (col == 4)
                                listAttributes[row - 1].Notes = columnText;
                            if (col == 5)
                                listAttributes[row - 1].DateOverhaul = columnText;
                            col++;
                        }
                    }
                    row++;
                    col = 0;
                }
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void UpdateDirPumpGroup()
        {
            if (!string.IsNullOrEmpty(dirPhotos))
                dirPumpGroup = Path.Combine(dirPhotos, Path.Combine(dtpOH.Value.ToString("yyyy-MM"), txtAssetNum.Text));
        }

        private void PbPart_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox pbPart = sender as PictureBox;
            var files = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            PumpAttributes attributes = listAttributes[(int)pbPart.Tag];

            attributes.ImageFilePath = files[0];

            if (!Directory.Exists(dirPumpGroup))
            {
                dirPhotos = Path.GetDirectoryName(files[0]); // Photos folders
                UpdateDirPumpGroup();
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

            if (!Directory.Exists(dirPumpGroup))
                Directory.CreateDirectory(dirPumpGroup);

            foreach (PumpAttributes attrib in listAttributes)
            {
                // 201705 Group 01-1 IMPELLER FRONT.JPG
                if (File.Exists(attrib.ImageFilePath))
                {
                    string fpOld = attrib.ImageFilePath;
                    attrib.DateOverhaul = dtpOH.Value.ToShortDateString();
                    attrib.PartUniqueID = $"{dtpOH.Value.ToString("yyyyMM")} {txtAssetNum.Text.Trim()} {attrib.PartName}";
                    attrib.WorkOrderNumber = txtWorkOrderNum.Text;
                    string fpNew = $"{Path.Combine(dirPumpGroup, attrib.PartUniqueID)}{Path.GetExtension(fpOld)}";

                    if (File.Exists(fpNew))
                    {
                        // do not delete if the old file name is exactly as new
                        File.Replace(fpOld, fpNew, Path.Combine(Path.GetDirectoryName(fpNew), $"Old-{Path.GetFileName(fpNew)}"));
                    }
                    else
                    {
                        File.Move(fpOld, fpNew);
                    }

                    attrib.ImageFilePath = fpNew;
                }
            }
        }

        private string getCsvFilePath()
        {
            return Path.Combine(dirPumpGroup, txtAssetNum.Text + ".csv");
        }

        private void btnCreateCsv_Click(object sender, EventArgs e)
        {
            // error checking
            if (string.IsNullOrEmpty(dirPumpGroup))
            {
                MessageBox.Show("Please drag and drop photos first.");
                return;
            }
            if (!string.IsNullOrEmpty(dirPumpGroup) && !Directory.Exists(dirPumpGroup))
            {
                Directory.CreateDirectory(dirPumpGroup);
            }

            string fpCsv = getCsvFilePath();

            if (File.Exists(fpCsv)) File.Delete(fpCsv);

            using (StreamWriter sw = new StreamWriter(fpCsv))
            {
                CsvSerializer.Serialize<PumpAttributes>(sw, listAttributes);
            }
        }

        private void btnCsvRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = dirPumpGroup;
            dlg.Filter = "Comma Separated Values (*.csv)|*.csv";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ReadCsv(dlg.FileName);
            }
        }

        private void txtAssetNum_TextChanged(object sender, EventArgs e)
        {
            UpdateDirPumpGroup();
        }

        private void dtpOH_ValueChanged(object sender, EventArgs e)
        {
            UpdateDirPumpGroup();
        }
    }
}