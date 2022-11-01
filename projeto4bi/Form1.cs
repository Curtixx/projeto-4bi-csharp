/* *******************************************************************
* Colegio Técnico Antônio Teixeira Fernandes (Univap)
* Curso Técnico em Informática - Data de Entrega: 26/10/2022
* Autores do Projeto: Henrique Curtis // Arthur Kinderman
* Turma: 2F
* Projeto 4° PVB
* Observação: 
* 
* projeto4bi.cs
* ************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace projeto4bi
{
    
    public partial class Form1 : Form
    {
        MySqlConnection connection = new MySqlConnection("SERVER=localhost;DATABASE=cadastro;UID=root;PASSWORD=;");
        //um DataReader é uma ampla categoria de objetos usados ​​para ler dados sequencialmente de uma fonte de dados
        MySqlDataReader dr = null;
        int cod = 0;
        string img;
        int indice = 1;
        string caminho = @"E:\Desktop\img_projeto\";
        public Form1()
        {
            InitializeComponent();
            
        }
        public string caption(string format)
        //PROCEDIMETO PARA COLOCAR EM MAIUSCULO TODAS AS PRIMEIRAS LETRAS DO NOME, EXCETO dos, das, de, da, do.
        {
            format = format.ToLower().Trim();
            string[] strs = { "dos ", "das ", "de ", "da ", "do " };
            for (int i = 0; i < format.Length; i++)
            {
                if (i == 0)
                {
                    format = char.ToUpper(format[0]) + format.Substring(1);
                }
                else
                {
                    if (i + 3 < format.Length && format[i - 1] == ' ' && (Array.IndexOf(strs, format.Substring(i, 4)) == -1 && Array.IndexOf(strs, format.Substring(i, 3)) == -1))
                    {
                        format = format.Substring(0, i - 1) + " " + char.ToUpper(format[i]) + "" + format.Substring(i + 1);
                    }
                }
            }
            return format;
        }
        public void salvar_img()
        {//PROCEDIMENTO APENAS PARA TRATAR TROCAR A EXTENSÃO DA IMAGEM
            
            openFileDialog1.FileName = "imagem";
            openFileDialog1.Title = "escolha uma imagem";
            openFileDialog1.Filter = "Image Files(*.JPG;*.PNG;*.JPEG;) |*.JPG;*.PNG;*.JPEG";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
            
            
        }
        public void consulta_ultimo_reg()
        {
            //PROCEDIMENTO PARA SELECIONAR O ULTIMO REGISTRO DO BANCO DE DADOS
            MySqlCommand query = connection.CreateCommand();
            connection.Open();
            string sql = "select * from tabela1";
            query = new MySqlCommand(sql, connection);
            dr = query.ExecuteReader();

            while (dr.Read())
            {
                textBox1.Text = dr.GetString(0);
                maskedTextBox2.Text = dr.GetString(1);
                textBox3.Text = dr.GetString(2);
                textBox4.Text = dr.GetString(3);
                textBox5.Text = dr.GetString(4);
                maskedTextBox1.Text = dr.GetString(5);
                pictureBox1.Load(caminho + dr.GetString(6));

            }
            connection.Close();
        }
        public void consulta_primeiro_reg()
        {
            // PROCEDIMENTO APENAS PARA CONSULTAR O ULTIMO REGISTRO DO BANCO DE DADOS
            MySqlCommand query = connection.CreateCommand();
            connection.Open();
            string sql = "select * from tabela1";
            query = new MySqlCommand(sql, connection);
            dr = query.ExecuteReader();

            if (dr.Read())
            {
                textBox1.Text = dr.GetString(0);
                maskedTextBox2.Text = dr.GetString(1);
                textBox3.Text = dr.GetString(2);
                textBox4.Text = dr.GetString(3);
                textBox5.Text = dr.GetString(4);
                maskedTextBox1.Text = dr.GetString(5);
                pictureBox1.Load(caminho + dr.GetString(6));


            }

            connection.Close();
        }
        public void consulta_espec()
        {
            // PROCEDIMENTO FEITO PAR COSULTAR O REGISTRO PELO CODIGO DESEJADO
            MySqlCommand query = connection.CreateCommand();
            connection.Open();
            string sqlS = "select * from tabela1 where codigo = '" + textBox1.Text + "'";
            query = new MySqlCommand(sqlS, connection);
            dr = query.ExecuteReader();
            int auxiliar = 0;
            while (dr.Read())
            {
                auxiliar++;
                pictureBox1.Visible = true;
                textBox1.Text = dr.GetString(0);
                maskedTextBox2.Text = dr.GetString(1);
                textBox3.Text = dr.GetString(2);
                textBox4.Text = dr.GetString(3);
                textBox5.Text = dr.GetString(4);
                maskedTextBox1.Text = dr.GetString(5);
                pictureBox1.Load(caminho + dr.GetString(6));


            }
            if (auxiliar== 0)
            {
                MessageBox.Show("NÃO EXISTE NENHUM REGISTRO COM ESSE CODIGO!");
                textBox1.Text = "";
                maskedTextBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                maskedTextBox1.Text = "";
                pictureBox1.Image = null;
            }
            
            connection.Close();
            dr.Close();
        }
        public void delet()
        {
            // PROCEDIMENTO FEITO PARA EXCLUSÃO DE UM REGISTRO CASO ELE EXISTA NO BANCO DE DADOS
            if (textBox1.Text == "")
            {
                MessageBox.Show("CODIGO INVALIDO!!");
                textBox1.Focus();
            }
            else
            {
                MessageBoxButtons botoes = MessageBoxButtons.YesNo;
                MessageBoxIcon icone = MessageBoxIcon.Question;
                DialogResult resp = new DialogResult();
                resp = MessageBox.Show("TEM CERTEZA DE DESEJA EXCLUIR?","ATENÇÃO!!!",botoes,icone);
                if (resp == DialogResult.Yes)
                {
                    MySqlCommand queryS = connection.CreateCommand();
                    connection.Open();
                    string sqlS = "select * from tabela1 where codigo = '" + textBox1.Text + "'";
                    queryS = new MySqlCommand(sqlS, connection);
                    dr = queryS.ExecuteReader();
                    int aux = 0;
                    while (dr.Read())
                    {
                        aux++;
                        pictureBox1.Visible = true;
                        textBox1.Text = dr.GetString(0);
                        maskedTextBox2.Text = dr.GetString(1);
                        textBox3.Text = dr.GetString(2);
                        textBox4.Text = dr.GetString(3);
                        textBox5.Text = dr.GetString(4);
                        maskedTextBox1.Text = dr.GetString(5);
                        pictureBox1.Load(caminho + dr.GetString(6));



                    }
                    dr.Close();
                    if (aux == 0)
                    {
                        MessageBox.Show("NÃO EXISTE NENHUM REGISTRO COM ESSE CODIGO PARA EXCLUIR!");
                        textBox1.Text = "";
                        maskedTextBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        maskedTextBox1.Text = "";

                    }
                    else
                    {
                        MySqlCommand query = connection.CreateCommand();

                        string sql = "delete from tabela1 where codigo = '" + textBox1.Text + "'";
                        query = new MySqlCommand(sql, connection);
                        query.ExecuteReader();
                        MessageBox.Show("DELETADO!");
                        textBox1.Text = "";
                        maskedTextBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        maskedTextBox1.Text = "";
                        pictureBox1.Image = null;

                    }

                    connection.Close();
                }
                
                
            }
            
        }

        public bool proximoOuAnterior()
        {

            MySqlCommand command = connection.CreateCommand();
            connection.Open();

            command.CommandText = $"SELECT * from tabela1";

            MySqlDataReader dr = command.ExecuteReader();

            int i = 1;

            while (dr.Read())
            {
                if (indice == i)
                {
                    pictureBox1.Visible = true;
                    textBox1.Text = dr.GetString(0);
                    maskedTextBox2.Text = dr.GetString(1);
                    textBox3.Text = dr.GetString(2);
                    textBox4.Text = dr.GetString(3);
                    textBox5.Text = dr.GetString(4);
                    maskedTextBox1.Text = dr.GetString(5);
                    pictureBox1.Load(caminho + dr.GetString(6));
                    connection.Close();
                    return true;
                }
                i++;
            }
            connection.Close();
            return false;
        }

        public void proximo()
        {
            if (indice == 0)
            {
                indice = -1;
            }
            else
            {
                indice++;
            }
            bool retorno = proximoOuAnterior();
            if (!retorno)
            {
                indice--;
            }
            connection.Close();
        }
        public void anterior()
        {
            if (indice == 0)
            {
                indice = 1;
            }
            else
            {
                indice--;
            }
            bool retorno = proximoOuAnterior();
            if (!retorno)
            {
                indice++;
            }
            connection.Close();
        }
        public void insert()
        {

            //PROCEDIMENTO FEITO PARA INSERÇÃO CASO OS CAMPOS FOREM VALIDADOS

            string nome = caption(textBox3.Text);
            string cidade = caption(textBox4.Text);
            string bairro = caption(textBox5.Text);
            string cpf = maskedTextBox2.Text.Replace(",", ".");
            //PATH:
            // Executa operações em instâncias de String que contêm informações de caminho de arquivo ou diretório.
            // Essas operações são executadas de uma maneira em plataforma cruzada.
            string extencao = Path.GetExtension(openFileDialog1.FileName);
            //GETEXTENSION:
            //	Retorna a extensão de um caminho de arquivo que é representado por um intervalo de caracteres somente leitura.
            img = textBox1.Text +"#"+ textBox3.Text + extencao;
                
            //CAMINHO DA PASTA
            pictureBox1.Image.Save(caminho + "" + img, pictureBox1.Image.RawFormat);
            //RAWFORMAT
            // Obtém o formato de arquivo deste Image.
                
            MySqlCommand query = connection.CreateCommand();

            connection.Open();
            string codigo = "select * from tabela1 where codigo = '" + textBox1.Text + "'";

            query = new MySqlCommand(codigo, connection);
                
            dr = query.ExecuteReader();
            int cont = 0;
            while (dr.Read())
            {
                cont++;

            }
            dr.Close();
            if (cont == 0)
            {

                    
                string sql = "insert into tabela1" +
                    " values" +
                    " ('" + textBox1.Text + "', '" + cpf + "', '" + nome + "', '" + cidade + "', '" + bairro + "', '" + maskedTextBox1.Text + "', '" + img + "')";
                query = new MySqlCommand(sql, connection);
                query.ExecuteReader();
                MessageBox.Show("INSERIDO!");
                connection.Close();

            }
            else
            {
                    
                string cpfUp = maskedTextBox2.Text.Replace(",", ".");
                string codigo2 = $"update tabela1 set cpf = '{cpfUp}', nome='{textBox3.Text}', cidade='{textBox4.Text}', bairro='{textBox5.Text}', telefone='{maskedTextBox1.Text}', imagem='{img}' where codigo = '{textBox1.Text}'";
                query = new MySqlCommand(codigo2, connection);
                query.ExecuteReader();
                MessageBox.Show("ALTERADO!");
                connection.Close();
            }
            connection.Close();
            
            
            
            

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == "")
            {
                MessageBox.Show("CAMPO DO CODIGO VAZIO!");
            }
            else
            {
                int cod = int.Parse(textBox1.Text);
            }
            
 
            if (cod <=0 && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && pictureBox1.Image == null)
            {
                
                MessageBox.Show("CAMPOS INVALIDOS!");
            }
            else
            {
                insert();
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            consulta_espec();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            salvar_img();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            consulta_primeiro_reg();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            consulta_ultimo_reg();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            delet();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            proximo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            anterior();
        }
    }
    }

