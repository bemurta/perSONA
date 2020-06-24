using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace perSONA
{
    public partial class calibrationHelp : Form
    {
        private readonly IvAInterface vAInterface;
        public calibrationHelp(IvAInterface vAInterface)
        {
            InitializeComponent();
            this.vAInterface = vAInterface;
            if (Properties.Settings.Default.CALIBRATION_MODE == "A1")
            {
                this.Text = "Ajuda de calibração com MNPS";
                Caso1.Text = "Etapa 1:" + "\n" + "A) Posicione o MNPS no centro do sistema de caixas. Configure o MNPS para medir sem ponderação em frequência." + "\n" +
                                                 "B) Eleve o MNPS a mesma altura das caixas (1,2 metros)." + "\n" +
                                                 "C) Para iniciar a calibração, faça o sistema reproduzir o sinal de calibração em cada um dos reprodutores." + "\n" +
                                                 "D) Ajuste o volume de reprodução individualmente em cada reprodutor até que o MNPS marque 94dB." + "\n" +
                                                 "E) Você pode reproduzir o sinal sonoro quantas vezes for necessário para você ajustar o NPS em 94 dB." + "\n" +
                                                 "F) Quando o MNPS marcar 94dB assinale a checkbox correspondente a caixa calibrada e clique em próximo.";

                Caso2.Text = "Etapa 2:" + "\n" + "A) Após a calibração dos reprodutores separadamente é necessária uma calibração a nível global." + "\n" +
                                                 "B) Para isso mantenha o MNPS na mesma posição" + "\n" +
                                                 "C) Para iniciar a calibração conjunta, o sistema reproduzirá o sinal de calibração em todos os reprodutores simultaneamente." + "\n" +
                                                 "E) Ajuste o volume de todos os reprodutores simultaneamente através da trackbar da tela de calibração até atingir os 94dB." + "\n" +
                                                 "E) Após isso finalize a calibração";

            }
            else if (Properties.Settings.Default.CALIBRATION_MODE == "A2")
            {
                this.Text = "Ajuda de calibração com IPhone e App";
                Caso1.Text = "Etapa 1:" + "\n" + "A) Conecte o microfone externo ao seu IPhone, e abra o aplicativo de calibração." + "\n" +
                                                 "B) Posicione o microfone no centro do sistema de caixas." + "\n" +
                                                 "C) Para iniciar a calibração, faça o sistema reproduzir o sinal de calibração em cada um dos reprodutores." + "\n" +
                                                 "D) Ajuste o volume de reprodução individualmente em cada reprodutor até que o aplicativo marque 94dB." + "\n" +
                                                 "E) Você pode reproduzir o sinal sonoro quantas vezes for necessário para você ajustar o NPS em 94 dB." + "\n" +
                                                 "F) Quando o o aplicativo marcar 94dB assinale a checkbox correspondente a caixa calibrada e prossiga;";

                Caso2.Text = "Etapa 2:" + "\n" + "A) Após a calibração dos reprodutores separadamente é necessária uma calibração a nível global." + "\n" +
                                                 "B) Para isso mantenha o microfone na mesma posição." + "\n" +
                                                 "C) Para iniciar a calibração conjunta, o sistema reproduzirá o sinal de calibração em todos os reprodutores simultaneamente." + "\n" +
                                                 "D) Ajuste o volume de todos os reprodutores simultaneamente através da trackbar da tela de calibração até atingir os 94dB." + "\n" +
                                                 "E) Após isso finalize a calibração";
            }
            else if (Properties.Settings.Default.CALIBRATION_MODE == "A3")
            {
                this.Text = "Ajuda de calibração com placa de som";
                Caso1.Text = "Etapa 1:" + "\n" + "A) Conecte o microfone externo ao seu calibrador" + "\n" +
                                                 "B) Posicione o microfone no centro do sistema de caixas" + "\n" +
                                                 "C) Eleve o microfone a mesma altura das caixas (1,2 metros)" + "\n" +
                                                 "D) Durante a calibração você poderá reproduzir um sinal sonoro" + "\n" +
                                                 "E) Ajuste o volume da caixa que está sendo calibrada até que o calibrador indique que está correto" + "\n" +
                                                 "F) Você pode executar o sinal sonoro quantas vezes forem necessárias" + "\n" +
                                                 "G) Quando a calibração de uma caixa for efetuada com sucesso, outra caixa será calibrada";

                Caso2.Text = "Etapa 2:" + "\n" + "A) Após a calibração das caixas separadamente é necessária uma calibração a nível global" + "\n" +
                                                 "B) Para isso mantenha o microfone na mesma posição" + "\n" +
                                                 "C) Da mesma forma que anteriormente você poderá reproduzir um sinal sonoro" + "\n" +
                                                 "D) O sinal sonoro agora será reproduzido em todas as caixas" + "\n" +
                                                 "E) Ajuste o volume de todos os reprodutores simultaneamente através da trackbar da tela de calibração até atingir os 94dB." + "\n" +
                                                 "F) Após isso a calibração terá sido efetuada com sucesso";
            }
            else if (Properties.Settings.Default.CALIBRATION_MODE == "B1")
            {
                this.Text = "Ajuda de calibração com orelha artifical";
                Caso1.Text = "Etapa 1:" + "\n" + "A) Posicione o fone de ouvido na orelha artificial" + "\n" +
                                                 "B) Para iniciar a calibração, faça o sistema reproduzir o sinal de calibração em cada um dos reprodutores." + "\n" +
                                                 "C) Ajuste o volume de reprodução individualmente em cada reprodutor até atingir a pressão sonora de 94dB." + "\n" +
                                                 "D) Para ajustar o volume dos fones individualmente siga os seguintes passos: Clique em \"tecla do windows + R\" ->" + "\n" +
                                                 "Digite \"mmsys.cpl\" (sem aspas) -> \"Ok\" -> Dê um duplo clique em \"Fones de ouvido\" -> \"Níveis\" -> \"Balanço\"" + "\n" +
                                                 "E) Você pode reproduzir o sinal sonoro quantas vezes for necessário para você ajustar o NPS em 94 dB." + "\n" +
                                                 "F) Quando o NPS for 94dB assinale a checkbox correspondente a caixa calibrada e clique em próximo.";

                Caso2.Text = "Etapa 2:" + "\n" + "A) Após a calibração dos reprodutores separadamente é necessária uma calibração a nível global." + "\n" +
                                                 "B) Para isso mantenha o fone de ouvido na mesma posição" + "\n" +
                                                 "C) Para iniciar a calibração conjunta, o sistema reproduzirá o sinal de calibração em todos os reprodutores simultaneamente." + "\n" + 
                                                 "D) Ajuste o volume de todos os reprodutores simultaneamente através da trackbar da tela de calibração até atingir os 94dB." + "\n" +
                                                 "E) Após isso finalize a calibração";
            }
            else
            {
                this.Text = "Ajuda de calibração com manequim";
                Caso1.Text = "Etapa 1:" + "\n" + "A) Posicione o fone de ouvido no manequim" + "\n" +
                                                 "B) Para iniciar a calibração, faça o sistema reproduzir o sinal de calibração em cada um dos reprodutores." + "\n" +
                                                 "C) Ajuste o volume de reprodução individualmente em cada reprodutor até atingir a pressão sonora de 94dB." + "\n" +
                                                 "D) Para ajustar o volume dos fones individualmente siga os seguintes passos: Clique em \"tecla do windows + R\" ->" + "\n" +
                                                 "Digite \"mmsys.cpl\" (sem aspas) -> \"Ok\" -> Dê um duplo clique em \"Fones de ouvido\" -> \"Níveis\" -> \"Balanço\"" + "\n" +
                                                 "E) Você pode reproduzir o sinal sonoro quantas vezes for necessário para você ajustar o NPS em 94 dB." + "\n" +
                                                 "F) Quando o NPS for 94dB assinale a checkbox correspondente a caixa calibrada e clique em próximo.";

                Caso2.Text = "Etapa 2:" + "\n" + "A) Após a calibração dos reprodutores separadamente é necessária uma calibração a nível global." + "\n" +
                                                 "B) Para isso mantenha o fone de ouvido na mesma posição" + "\n" +
                                                 "C) Para iniciar a calibração conjunta, o sistema reproduzirá o sinal de calibração em todos os reprodutores simultaneamente." + "\n" +
                                                 "D) Ajuste o volume de todos os reprodutores simultaneamente através da trackbar da tela de calibração até atingir os 94dB." + "\n" +
                                                 "E) Após isso finalize a calibração";
            }
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CALIBRATION_MODE == "B1" | Properties.Settings.Default.CALIBRATION_MODE == "B2")
            {
                new earphoneCalibration(vAInterface).Show();
                Close();
            }
            else
            {
                new speakerCalibration(vAInterface).Show();
                Close();
            }
        }
    }
}