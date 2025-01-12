using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinancialCrm.Models;

namespace FinancialCrm
{
    public partial class FrmBilling : Form
    {
        public FrmBilling()
        {
            InitializeComponent();
        }
        FinancialCrmDbEntities db = new FinancialCrmDbEntities();
        private void GetList()
        {
            var values = db.Bills.ToList();
            dataGridView1.DataSource = values;
        }
        private void FrmBilling_Load(object sender, EventArgs e)
        {
            GetList();
        }

        private void btnBillList_Click(object sender, EventArgs e)
        {
            GetList();
        }

        private void btnCreateBill_Click(object sender, EventArgs e)
        {
            string title = txtBillTitle.Text;
            decimal amount = decimal.Parse(txtBillAmount.Text);
            string period = txtBillPeriod.Text;

            Bills bills = new Bills();
            bills.BillTitle = title;
            bills.BillAmount = amount;
            bills.BillPeriod = period;
            db.Bills.Add(bills);
            db.SaveChanges();
            MessageBox.Show("Ödeme Başarılı Bir Şekilde Eklendi", "Ödeme - Faturalar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GetList();
        }

        private void btnRemoveBill_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtBillId.Text);
            var removeValue = db.Bills.Find(id);
            db.Bills.Remove(removeValue);
            db.SaveChanges();
            MessageBox.Show("Ödeme Başarılı Bir Şekilde Silindi","Ödeme-Faturalar",MessageBoxButtons.OK, MessageBoxIcon.Information);
            GetList();
        }

        private void btnUpdateBill_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtBillId.Text);
            var updatedBill = db.Bills.Find(id);
            if (updatedBill != null)
            {
                updatedBill.BillTitle = txtBillTitle.Text;
                updatedBill.BillAmount = decimal.Parse(txtBillAmount.Text);
                updatedBill.BillPeriod = txtBillPeriod.Text;
                db.SaveChanges();
                MessageBox.Show("Ödeme Başarılı Bir Şekilde Güncellendi", "Ödeme - Faturalar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetList();
            }
            else
            {
                MessageBox.Show("Ödeme Bulunamadı", "Ödeme - Faturalar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GetList();
            }
        }

        private void btnBanksForm_Click(object sender, EventArgs e)
        {
            FrmBanks frmBanks = new FrmBanks();
            frmBanks.Show();
            this.Hide();
        }
    }
}
