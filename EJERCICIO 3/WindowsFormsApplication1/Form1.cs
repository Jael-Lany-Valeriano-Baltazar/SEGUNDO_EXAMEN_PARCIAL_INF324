using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private string userInput;
        private Bitmap bmp;
        int cR, cG, cB;
        int r2, g2, b2;//ncolor
        int pR, pG, pB;
        DataTable dato = null;
        public Color customColor;
        private SqlConnection conexion = new SqlConnection("Data Source=localhost;Initial Catalog=DB_TEXTURA;Integrated Security=True");
       
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Bitmap bmp;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                bmp = new Bitmap(openFileDialog1.FileName);
                bmp = new Bitmap(bmp, new Size(150, 150));
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Image = bmp;
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

            Color c = new Color();
            pR = 0; pG = 0; pB = 0;
            for (int ki = e.X; ki < e.X + 10; ki++)
            {
                for (int kj = e.Y; kj < e.Y + 10; kj++)
                {
                    c = bmp.GetPixel(ki, kj);
                    pR = pR + c.R;
                    pG = pG + c.G;
                    pB = pB + c.B;
                }
            }
            pR = pR / 100;
            pG = pG / 100;
            pB = pB / 100;
            textBox1.Text = pR.ToString();
            textBox2.Text = pG.ToString();
            textBox3.Text = pB.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*Bitmap bmp = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Color c = new Color();
            for (int i=0;i<bmp.Width;i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i,j);
                    bmp2.SetPixel(i, j, Color.FromArgb(c.R, 0, 0));
                }
            pictureBox2.Image = bmp2;*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*Bitmap bmp = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Color c = new Color();
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    bmp2.SetPixel(i, j, Color.FromArgb(0, c.G, 0));
                }
            pictureBox2.Image = bmp2;*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*Bitmap bmp = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Color c = new Color();
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    bmp2.SetPixel(i, j, Color.FromArgb(0, 0, c.B));
                }
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.Image = bmp2;*/
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
            CustomInputDialog dialog = new CustomInputDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                userInput = dialog.InputText;

                // Guardar el valor ingresado
                if (userInput.Length != 0)
                {
                    // Aquí puedes realizar la lógica para guardar el valor en alguna variable o en una base de datos, por ejemplo
                    MessageBox.Show($"COLOR GUARDADO: {userInput}", "COLOR", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Guardar(userInput, int.Parse(textBox6.Text), int.Parse(textBox5.Text), int.Parse(textBox4.Text));
                    //Listar();
                }
                else
                {
                    MessageBox.Show("No se ingresó ningún dato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

       
        private void button7_Click(object sender, EventArgs e)
        {

            Colorear();

        }

        private void button12_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM RGB_COLOR WHERE NOM_COLOR = '" + textBox10.Text + "'", conexion);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "datos");

            if (ds.Tables["datos"].Rows.Count > 0)
            {


                textBox9.Text = ds.Tables["datos"].Rows[0][2].ToString();
                textBox8.Text = ds.Tables["datos"].Rows[0][3].ToString();
                textBox7.Text = ds.Tables["datos"].Rows[0][4].ToString();

                button7.ForeColor = Color.FromArgb(int.Parse(textBox9.Text), int.Parse(textBox8.Text), int.Parse(textBox7.Text)); //colorDialog1.Color;
            }
            else
            {
                MessageBox.Show("No existe el registro", "RGB_COLOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }

        private void button11_Click(object sender, EventArgs e)
        {

            CustomInputDialog dialog = new CustomInputDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                userInput = dialog.InputText;

                // Guardar el valor ingresado
                if (userInput.Length != 0)
                {
                    MessageBox.Show($"COLOR GUARDADO: {userInput}", "NUEVO COLOR", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Guardar(userInput, int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox3.Text));
                    
                }
                else
                {
                    MessageBox.Show("No se ingresó ningún dato.", "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //textBox6.Text = textBox9.Text
        }


        /// ///////////////////// COLOREAR ////////////////////////////////
        private void Colorear()
        {
            
            int rr, gg, bb;
                if (int.TryParse(textBox9.Text, out rr) &&
                    int.TryParse(textBox8.Text, out gg) &&
                    int.TryParse(textBox7.Text, out bb))
                {
                    customColor = Color.FromArgb(rr, gg, bb);

                }

                int mR = 0, mG = 0, mB = 0;
                int cR = 0, cG = 0, cB = 0;
                Color c = new Color();
                Bitmap bmpR = new Bitmap(bmp.Width, bmp.Height);
                for (int i = 0; i < bmp.Width - 10; i = i + 10)
                {
                    for (int j = 0; j < bmp.Height - 10; j = j + 10)
                    {
                        mR = 0; mG = 0; mB = 0;
                        for (int ki = i; ki < i + 10; ki++)
                        {
                            for (int kj = j; kj < j + 10; kj++)
                            {
                                c = bmp.GetPixel(ki, kj);
                                mR = mR + c.R;
                                mG = mG + c.G;
                                mB = mB + c.B;
                            }
                        }
                        mR = mR / 100;
                        mG = mG / 100;
                        mB = mB / 100;



                        c = bmp.GetPixel(i, j);
                        if ((pR - 10 <= mR && mR <= pR + 10) && (pG - 10 <= mG && mG <= pG + 10) && (pB - 10 <= mB && mB <= pB + 10))
                        {
                            for (int ki = i; ki < i + 10; ki++)
                            {
                                for (int kj = j; kj < j + 10; kj++)
                                {
                                    bmpR.SetPixel(ki, kj, customColor);
                                    cR = c.R;
                                    cG = c.G;
                                    cB = c.B;


                                }
                            }
                        }
                        else
                        {
                            for (int ki = i; ki < i + 10; ki++)
                            {
                                for (int kj = j; kj < j + 10; kj++)
                                {
                                    c = bmp.GetPixel(ki, kj);
                                    bmpR.SetPixel(ki, kj, Color.FromArgb(c.R, c.G, c.B));
                                    
                                }
                            }
                        }


                    }

                }
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.Image = bmpR;
        }



        private void button9_Click(object sender, EventArgs e)
        {
            
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK){                
                int r = colorDialog1.Color.R; 
                int g = colorDialog1.Color.G; 
                int b = colorDialog1.Color.B;                
                textBox6.Text = r.ToString();                textBox5.Text = g.ToString();
                textBox4.Text = b.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        public void Guardar(string nom, int r1, int g1, int b1)
        {
            try
            {
                SqlCommand comand = new SqlCommand("INSERT INTO RGB_COLOR(NOM_COLOR, R1, G1, B1)  VALUES(@nom, @r1, @g1, @b1)", conexion);
comand.Parameters.Add(new SqlParameter("@nom", nom));
                comand.Parameters.Add(new SqlParameter("@r1", r1));
                comand.Parameters.Add(new SqlParameter("@g1", g1));
                comand.Parameters.Add(new SqlParameter("@b1", b1));
                conexion.Open();
                comand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            { conexion.Close(); }
        }


        

    }




    public class CustomInputDialog : Form
    {
        private TextBox textBoxInput;
        private Button buttonOk;

        public string InputText => textBoxInput.Text;

        public CustomInputDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            textBoxInput = new TextBox();
            buttonOk = new Button();

            // Configurar el TextBox
            textBoxInput.Location = new System.Drawing.Point(12, 12);
            textBoxInput.Size = new System.Drawing.Size(200, 20);

            // Configurar el botón OK
            buttonOk.Location = new System.Drawing.Point(12, 40);
            buttonOk.Size = new System.Drawing.Size(75, 23);
            buttonOk.Text = "GUARDAR";
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Click += ButtonOk_Click;

            // Configurar la ventana de diálogo
            this.ClientSize = new System.Drawing.Size(224, 75);
            this.Controls.Add(textBoxInput);
            this.Controls.Add(buttonOk);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "COLOR";
            this.ResumeLayout(false);
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        


    }



}
