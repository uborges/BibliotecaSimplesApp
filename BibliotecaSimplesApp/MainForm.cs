// Arquivo: MainForm.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BibliotecaSimplesApp.Models; // Adicionado para acessar os modelos

namespace BibliotecaSimplesApp
{
    public partial class MainForm : Form
    {
        private List<Categoria> categorias = new List<Categoria>();
        private List<Livro> livros = new List<Livro>();
        private List<Membro> membros = new List<Membro>();
        private List<Emprestimo> emprestimos = new List<Emprestimo>();

        private ListBox lstCategorias;
        private TextBox txtNomeCategoria;
        private Button btnAddCategoria;

        private ListBox lstLivros;
        private TextBox txtTituloLivro;
        private TextBox txtAutorLivro;
        private NumericUpDown numAnoLivro;
        private TextBox txtISBNLivro;
        private ComboBox cmbCategoriaLivro;
        private Button btnAddLivro;

        private ListBox lstMembros;
        private TextBox txtNomeMembro;
        private TextBox txtEmailMembro;
        private Button btnAddMembro;

        private ListBox lstEmprestimosAtivos;
        private ComboBox cmbLivrosDisponiveis;
        private ComboBox cmbMembrosEmprestimo;
        private Button btnEmprestar;
        private Button btnDevolver;

        private RichTextBox rtbDetalhes;

        public MainForm()
        {
            // O Visual Studio normalmente chama InitializeComponent() aqui,
            // que é preenchido pelo designer.
            // Para este exemplo, usamos nosso método customizado.
            InitializeComponentCustom();
            CarregarDadosIniciais();
            AtualizarListas();
        }

        // Este método normalmente seria o conteúdo de MainForm.Designer.cs
        // e chamado por InitializeComponent().
        private void InitializeComponentCustom()
        {
            this.Text = "Sistema de Biblioteca Simples";
            this.Size = new System.Drawing.Size(950, 700);
            this.Font = new Font("Segoe UI", 9F);
            this.StartPosition = FormStartPosition.CenterScreen;

            // --- Painel Categorias ---
            GroupBox gbCategorias = new GroupBox { Text = "Categorias", Location = new Point(10, 10), Size = new Size(280, 200), Anchor = AnchorStyles.Top | AnchorStyles.Left };
            lstCategorias = new ListBox { Location = new Point(10, 20), Size = new Size(260, 100), DisplayMember = "Nome", Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            lstCategorias.SelectedIndexChanged += (s, e) => ExibirDetalhesSelecionado(lstCategorias.SelectedItem);
            txtNomeCategoria = new TextBox { Location = new Point(10, 130), Size = new Size(170, 23), PlaceholderText = "Nome da Categoria", Anchor = AnchorStyles.Top | AnchorStyles.Left };
            btnAddCategoria = new Button { Text = "Adicionar", Location = new Point(190, 128), Size = new Size(80, 27), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnAddCategoria.Click += BtnAddCategoria_Click;
            gbCategorias.Controls.AddRange(new Control[] { lstCategorias, txtNomeCategoria, btnAddCategoria });

            // --- Painel Livros ---
            GroupBox gbLivros = new GroupBox { Text = "Livros", Location = new Point(300, 10), Size = new Size(300, 350), Anchor = AnchorStyles.Top | AnchorStyles.Left };
            lstLivros = new ListBox { Location = new Point(10, 20), Size = new Size(280, 120), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            lstLivros.SelectedIndexChanged += (s, e) => ExibirDetalhesSelecionado(lstLivros.SelectedItem);
            txtTituloLivro = new TextBox { Location = new Point(10, 150), Size = new Size(280, 23), PlaceholderText = "Título do Livro", Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            txtAutorLivro = new TextBox { Location = new Point(10, 180), Size = new Size(280, 23), PlaceholderText = "Autor", Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            numAnoLivro = new NumericUpDown { Location = new Point(10, 210), Size = new Size(100, 23), Minimum = 1000, Maximum = DateTime.Now.Year, Value = DateTime.Now.Year, Anchor = AnchorStyles.Top | AnchorStyles.Left };
            txtISBNLivro = new TextBox { Location = new Point(120, 210), Size = new Size(170, 23), PlaceholderText = "ISBN", Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            Label lblCatLivro = new Label { Text = "Categoria:", Location = new Point(10, 243), Size = new Size(70, 20), Anchor = AnchorStyles.Top | AnchorStyles.Left };
            cmbCategoriaLivro = new ComboBox { Location = new Point(80, 240), Size = new Size(210, 23), DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            btnAddLivro = new Button { Text = "Adicionar Livro", Location = new Point(10, 270), Size = new Size(280, 30), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            btnAddLivro.Click += BtnAddLivro_Click;
            gbLivros.Controls.AddRange(new Control[] { lstLivros, txtTituloLivro, txtAutorLivro, numAnoLivro, txtISBNLivro, lblCatLivro, cmbCategoriaLivro, btnAddLivro });

            // --- Painel Membros ---
            GroupBox gbMembros = new GroupBox { Text = "Membros", Location = new Point(10, 220), Size = new Size(280, 200), Anchor = AnchorStyles.Top | AnchorStyles.Left };
            lstMembros = new ListBox { Location = new Point(10, 20), Size = new Size(260, 100), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            lstMembros.SelectedIndexChanged += (s, e) => ExibirDetalhesSelecionado(lstMembros.SelectedItem);
            txtNomeMembro = new TextBox { Location = new Point(10, 130), Size = new Size(170, 23), PlaceholderText = "Nome do Membro", Anchor = AnchorStyles.Top | AnchorStyles.Left };
            txtEmailMembro = new TextBox { Location = new Point(10, 160), Size = new Size(170, 23), PlaceholderText = "Email", Anchor = AnchorStyles.Top | AnchorStyles.Left };
            btnAddMembro = new Button { Text = "Adicionar", Location = new Point(190, 143), Size = new Size(80, 30), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnAddMembro.Click += BtnAddMembro_Click;
            gbMembros.Controls.AddRange(new Control[] { lstMembros, txtNomeMembro, txtEmailMembro, btnAddMembro });

            // --- Painel Empréstimos ---
            GroupBox gbEmprestimos = new GroupBox { Text = "Empréstimos", Location = new Point(610, 10), Size = new Size(310, 350), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            Label lblLivDisp = new Label { Text = "Livro Disponível:", Location = new Point(10, 28), Size = new Size(100, 20), Anchor = AnchorStyles.Top | AnchorStyles.Left };
            cmbLivrosDisponiveis = new ComboBox { Location = new Point(110, 25), Size = new Size(190, 23), DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            Label lblMembEmp = new Label { Text = "Membro:", Location = new Point(10, 58), Size = new Size(100, 20), Anchor = AnchorStyles.Top | AnchorStyles.Left };
            cmbMembrosEmprestimo = new ComboBox { Location = new Point(110, 55), Size = new Size(190, 23), DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            btnEmprestar = new Button { Text = "Emprestar", Location = new Point(10, 85), Size = new Size(140, 30), Anchor = AnchorStyles.Top | AnchorStyles.Left };
            btnEmprestar.Click += BtnEmprestar_Click;
            btnDevolver = new Button { Text = "Devolver Selecionado", Location = new Point(160, 85), Size = new Size(140, 30), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            btnDevolver.Click += BtnDevolver_Click;
            Label lblEmpAtivos = new Label { Text = "Empréstimos Ativos:", Location = new Point(10, 123), Size = new Size(150, 20), Anchor = AnchorStyles.Top | AnchorStyles.Left };
            lstEmprestimosAtivos = new ListBox { Location = new Point(10, 145), Size = new Size(290, 195), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right };
            lstEmprestimosAtivos.SelectedIndexChanged += (s, e) => ExibirDetalhesSelecionado(lstEmprestimosAtivos.SelectedItem);
            gbEmprestimos.Controls.AddRange(new Control[] { lblLivDisp, cmbLivrosDisponiveis, lblMembEmp, cmbMembrosEmprestimo, btnEmprestar, btnDevolver, lblEmpAtivos, lstEmprestimosAtivos });

            // --- Painel Detalhes ---
            GroupBox gbDetalhes = new GroupBox { Text = "Detalhes", Location = new Point(10, 430), Size = new Size(910, 220), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right };
            rtbDetalhes = new RichTextBox { Location = new Point(10, 20), Size = new Size(890, 190), ReadOnly = true, Font = new Font("Consolas", 10F), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right };
            gbDetalhes.Controls.Add(rtbDetalhes);

            this.Controls.AddRange(new Control[] { gbCategorias, gbLivros, gbMembros, gbEmprestimos, gbDetalhes });
            this.MinimumSize = new Size(950, 700); // Define um tamanho mínimo para evitar que os controles fiquem sobrepostos demais
        }


        private void CarregarDadosIniciais()
        {
            Categoria catFiccao = new Categoria("Ficção Científica");
            Categoria catHistoria = new Categoria("História");
            Categoria catProg = new Categoria("Programação");
            categorias.AddRange(new[] { catFiccao, catHistoria, catProg });

            Livro livro1 = new Livro("Duna", 1965, catFiccao, "Frank Herbert", "978-0441172719");
            Livro livro2 = new Livro("Sapiens", 2011, catHistoria, "Yuval Noah Harari", "978-0062316097");
            Livro livro3 = new Livro("Clean Code", 2008, catProg, "Robert C. Martin", "978-0132350884");
            Livro livro4 = new Livro("O Guia do Mochileiro das Galáxias", 1979, catFiccao, "Douglas Adams", "978-0345391803");
            livros.AddRange(new[] { livro1, livro2, livro3, livro4 });
            catFiccao.AdicionarItem(livro1);
            catHistoria.AdicionarItem(livro2);
            catProg.AdicionarItem(livro3);
            catFiccao.AdicionarItem(livro4);

            Membro membro1 = new Membro("Alice Silva", "alice@email.com");
            Membro membro2 = new Membro("Bruno Costa", "bruno@email.com");
            membros.AddRange(new[] { membro1, membro2 });
        }

        private void AtualizarListas()
        {
            lstCategorias.DataSource = null;
            lstCategorias.DataSource = categorias;
            lstCategorias.DisplayMember = "Nome";

            cmbCategoriaLivro.DataSource = null;
            cmbCategoriaLivro.DataSource = categorias;
            cmbCategoriaLivro.DisplayMember = "Nome";

            lstLivros.DataSource = null;
            lstLivros.DataSource = livros.ToList(); // Usar ToList() para criar uma nova cópia para o DataSource

            lstMembros.DataSource = null;
            lstMembros.DataSource = membros.ToList();

            cmbLivrosDisponiveis.DataSource = null;
            cmbLivrosDisponiveis.DataSource = livros.Where(l => l is ILocavel il && !il.EstaEmprestado).ToList();

            cmbMembrosEmprestimo.DataSource = null;
            cmbMembrosEmprestimo.DataSource = membros.ToList();

            lstEmprestimosAtivos.DataSource = null;
            lstEmprestimosAtivos.DataSource = emprestimos.Where(e => e.DataDevolucaoReal == null).ToList();
        }

        private void BtnAddCategoria_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNomeCategoria.Text))
            {
                categorias.Add(new Categoria(txtNomeCategoria.Text));
                txtNomeCategoria.Clear();
                AtualizarListas();
            }
            else
            {
                MessageBox.Show("Por favor, insira o nome da categoria.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnAddLivro_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTituloLivro.Text) ||
                string.IsNullOrWhiteSpace(txtAutorLivro.Text) ||
                cmbCategoriaLivro.SelectedItem == null)
            {
                MessageBox.Show("Preencha Título, Autor e selecione uma Categoria.", "Campos Obrigatórios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Categoria catSelecionada = cmbCategoriaLivro.SelectedItem as Categoria;
            if (catSelecionada != null)
            {
                Livro novoLivro = new Livro(
                    txtTituloLivro.Text,
                    (int)numAnoLivro.Value,
                    catSelecionada,
                    txtAutorLivro.Text,
                    txtISBNLivro.Text
                );
                livros.Add(novoLivro);
                // A lógica de AdicionarItem na Categoria já cuida da associação bidirecional
                // catSelecionada.AdicionarItem(novoLivro); // Removido pois o construtor do Livro já associa

                txtTituloLivro.Clear();
                txtAutorLivro.Clear();
                txtISBNLivro.Clear();
                numAnoLivro.Value = DateTime.Now.Year;
                cmbCategoriaLivro.SelectedIndex = -1;
                AtualizarListas();
            }
        }

        private void BtnAddMembro_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNomeMembro.Text) && !string.IsNullOrWhiteSpace(txtEmailMembro.Text))
            {
                // Validação simples de email (pode ser melhorada)
                if (!txtEmailMembro.Text.Contains("@") || !txtEmailMembro.Text.Contains("."))
                {
                    MessageBox.Show("Por favor, insira um email válido.", "Email Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                membros.Add(new Membro(txtNomeMembro.Text, txtEmailMembro.Text));
                txtNomeMembro.Clear();
                txtEmailMembro.Clear();
                AtualizarListas();
            }
            else
            {
                MessageBox.Show("Por favor, insira nome e email do membro.", "Campos Obrigatórios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEmprestar_Click(object sender, EventArgs e)
        {
            Livro livroSelecionado = cmbLivrosDisponiveis.SelectedItem as Livro;
            Membro membroSelecionado = cmbMembrosEmprestimo.SelectedItem as Membro;

            if (livroSelecionado == null || membroSelecionado == null)
            {
                MessageBox.Show("Selecione um livro disponível e um membro.", "Seleção Necessária", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Emprestimo novoEmprestimo = new Emprestimo(membroSelecionado, livroSelecionado);
                emprestimos.Add(novoEmprestimo);

                rtbDetalhes.Text = $"Empréstimo realizado:\n{livroSelecionado.Titulo} para {membroSelecionado.Nome}\nDevolução prevista: {novoEmprestimo.DataDevolucaoPrevista:dd/MM/yyyy}";
                AtualizarListas();
            }
            catch (InvalidOperationException exOp)
            {
                MessageBox.Show(exOp.Message, "Operação Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentNullException exArg)
            {
                MessageBox.Show(exArg.Message, "Argumento Nulo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDevolver_Click(object sender, EventArgs e)
        {
            Emprestimo emprestimoSelecionado = lstEmprestimosAtivos.SelectedItem as Emprestimo;

            if (emprestimoSelecionado == null)
            {
                MessageBox.Show("Selecione um empréstimo ativo para devolver.", "Seleção Necessária", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                emprestimoSelecionado.RegistrarDevolucao();
                rtbDetalhes.Text = $"Livro '{emprestimoSelecionado.Livro.Titulo}' devolvido por {emprestimoSelecionado.Membro.Nome} em {emprestimoSelecionado.DataDevolucaoReal:dd/MM/yyyy HH:mm}.";
                AtualizarListas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao devolver: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExibirDetalhesSelecionado(object itemSelecionado)
        {
            if (itemSelecionado == null)
            {
                rtbDetalhes.Clear();
                return;
            }

            if (itemSelecionado is ItemBiblioteca itemBib)
            {
                rtbDetalhes.Text = itemBib.ExibirDetalhes();
            }
            else if (itemSelecionado is Membro membro)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"MEMBRO: {membro.Nome}");
                sb.AppendLine($"Email: {membro.Email}");
                sb.AppendLine($"ID: {membro.Id}");
                sb.AppendLine("\nEmpréstimos Ativos:");
                var emprestimosDoMembro = emprestimos.Where(em => em.MembroId == membro.Id && em.DataDevolucaoReal == null);
                if (emprestimosDoMembro.Any())
                {
                    foreach (var emp in emprestimosDoMembro)
                    {
                        sb.AppendLine($"- {emp.Livro.Titulo} (Devolver até: {emp.DataDevolucaoPrevista:dd/MM/yy})");
                    }
                }
                else
                {
                    sb.AppendLine("(Nenhum empréstimo ativo)");
                }
                rtbDetalhes.Text = sb.ToString();
            }
            else if (itemSelecionado is Categoria categoria)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"CATEGORIA: {categoria.Nome}");
                sb.AppendLine($"ID: {categoria.Id}");
                sb.AppendLine("\nItens nesta categoria:");
                if (categoria.Itens.Any())
                {
                    foreach (var item in categoria.Itens)
                    {
                        sb.AppendLine($"- {item.Titulo} (ID: {item.Id})");
                    }
                }
                else
                {
                    sb.AppendLine("(Nenhum item cadastrado)");
                }
                rtbDetalhes.Text = sb.ToString();
            }
            else if (itemSelecionado is Emprestimo emprestimo)
            {
                rtbDetalhes.Text = $"EMPRÉSTIMO ID: {emprestimo.Id}\n" +
                                  $"Livro: {emprestimo.Livro.Titulo} (ID Livro: {emprestimo.LivroId})\n" +
                                  $"Membro: {emprestimo.Membro.Nome} (ID Membro: {emprestimo.MembroId})\n" +
                                  $"Data Empréstimo: {emprestimo.DataEmprestimo:dd/MM/yyyy HH:mm}\n" +
                                  $"Devolução Prevista: {emprestimo.DataDevolucaoPrevista:dd/MM/yyyy}\n" +
                                  $"Devolvido em: {(emprestimo.DataDevolucaoReal.HasValue ? emprestimo.DataDevolucaoReal.Value.ToString("dd/MM/yyyy HH:mm") : "PENDENTE")}";
            }
            else
            {
                rtbDetalhes.Text = itemSelecionado.ToString();
            }
        }
    }
}
