using FinancialCrm.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FinancialCrm
{
    public partial class FrmBanks : Form
    {
        public FrmBanks()
        {
            InitializeComponent();
        }
        FinancialCrmDbEntities db = new FinancialCrmDbEntities();

        private void FrmBanks_Load(object sender, EventArgs e)
        {
            //Banka Bakiyeleri Sorgulama

            var ziraatBankBalance = db.Banks.Where(x => x.BankTitle == "Ziraat Bankası").Select(y => y.BankBalance).FirstOrDefault();
            var vakifBankBalance = db.Banks.Where(x => x.BankTitle == "Vakıfbank").Select(y => y.BankBalance).FirstOrDefault();
            var isBankasiBalance = db.Banks.Where(x => x.BankTitle == "İş Bankası").Select(y => y.BankBalance).FirstOrDefault();
            var garantiBankBalance = db.Banks.Where(x => x.BankTitle == "Garanti Bankası").Select(y => y.BankBalance).FirstOrDefault();

            lblZiraatBankBalance.Text = ziraatBankBalance.ToString() + ".-TL";
            lblIsBankasiBalance.Text = isBankasiBalance.ToString() + ".-TL";
            lblVakifBankBalance.Text = vakifBankBalance.ToString() + ".-TL";

            // Retrieve the last 5 bank processes from the database in descending order
            var bankProcesses = db.BankProcesses.OrderByDescending(x => x.BankProcessId).Take(5).ToList();

            // Create a string to store the formatted bank process information
            string bankProcessInfo = "";

            // Iterate through the bank processes and format the information
            foreach (var bankProcess in bankProcesses)
            {
                bankProcessInfo += bankProcess.Description + " / " + bankProcess.ProcessAmount + ".-TL / " + bankProcess.ProcessDate + " \n";
                bankProcessInfo += "-----------------------------------------------------------------------------------------------------\n";
            }
            lblBankProcess1.Text = bankProcessInfo;
        }

        private void btnBillsForm_Click(object sender, EventArgs e)
        {
            FrmBilling frmBilling = new FrmBilling();
            frmBilling.Show();
            this.Hide();
        }
    }
}
