using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OOD
{
    public class User
    {
        public int type;
        public int id;
        public string pass;
        public DateTime dateCreated;
        public User(int id, int type, string pass, DateTime date)
        {
            this.id = id;
            this.type = type;
            this.pass = pass;
            this.dateCreated = date;
        }
    }
    public class DangNhap
    {
        string Username, Password;
        public DangNhap(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;

        }
    }
    public class Bill
    {
        public int id;
        public int idnv;
        public string nameCustomer, address, phoneNumber, dateCreated, installment;
        public Bill(int id, int idnv, string nameCustomer, string address, string phoneNumber, string dateCreated, string installment)
        {
            this.id = idnv;
            this.idnv = idnv;
            this.nameCustomer = nameCustomer;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.dateCreated = dateCreated;
            this.installment = installment;

        }
        /// <summary>
        /// Get Bill imformation
        /// </summary>
        /// <returns>a string seperate by |</returns>
        public string printBill()
        {
            return id + "|" + idnv + "|" + nameCustomer + "|" + address + "|" + phoneNumber + "|" + dateCreated + "|" + installment;
        }

    }
    public class SanPham
    {
        public int id;
        public string name;
        public string price;
        public int installmentPer;
        public int ammount;
        public string company;
        /// <summary>
        /// Initialize the product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="company"></param>
        /// <param name="price"></param>
        /// <param name="installment"></param>
        public SanPham(int id, string name, string company, string price, int installment)
        {
            this.id = id;
            this.name = name;
            this.company = company;
            this.price = price;
            this.installmentPer = installment;

        }
        public SanPham(SqlCommand command)
        {

        }
        /// <summary>
        /// Get all the legit imformation (Only admin can access this)
        /// </summary>
        /// <returns>a string of infomation</returns>
        public string introduce()
        {
            return name + "/" + ammount + "/" + price + "/" + company + "/" + installmentPer;
        }

        /// <summary>
        /// Sync and override this product db to database
        /// </summary>
        /// <param name="command">Commander</param>
        /// <param name="table">name of table in database</param>
        private void saveDB(SqlCommand command, string table)
        {
            command.CommandText = "Update " + table + " set ";
            command.ExecuteNonQuery();
        }
    }
    public class Provider
    {
        public int idPro;
        public string name, address;
        public DateTime dateCreated;
        public string phoneNumber;
        /// <summary>
        /// Initialize provider
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="dateCreated"></param>
        /// <param name="phoneNumber"></param>
        public Provider(int id, string name, string address, DateTime dateCreated,
            string phoneNumber)
        {
            this.idPro = id;
            this.name = name;
            this.address = address;
            this.dateCreated = dateCreated;
            this.phoneNumber = phoneNumber;
        }
        public string print()
        {
            return idPro + "|" + name + "|" + address + "|" + dateCreated.ToString()
                + "|" + phoneNumber;
        }
    }
    public class Kho
    {
        public List<SanPham> items;
        public List<Provider> providers;
        public List<User> users;
        /// <summary>
        /// Initialize Kho
        /// </summary>
        /// <param name="items"></param>
        /// <param name="providers"></param>
        /// <param name="users"></param>
        public Kho(List<SanPham> items,
            List<Provider> providers,
            List<User> users)
        {
            this.users = users;
            this.items = items;
            this.providers = providers;
        }
    }
    public class Message
    {
        private int idNV;
        private string NoiDung;
        private bool daDuyet;
        public Message(int idNV,string noidung,bool duyet)
        {
            this.idNV = idNV;
            this.NoiDung = noidung;
            this.daDuyet = daDuyet;
        }
        public string getMessage()
        {
            return this.NoiDung;
        }
        private void duyetTin()
        {
            daDuyet = true;
        }

    }
}
