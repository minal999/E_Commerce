using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using Aes = System.Security.Cryptography.Aes;

namespace E_Commerce.Models
{
    public class UserDAL
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
        SqlDataReader dr;
        SqlCommand cmd;

        public UserDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("DefaultConnection"));
        }
        public int UserRegister(User user)
        {
            user.Role_Id = 2;
            string qry = "insert into users values(@fname,@lname,@email,@contact,@password,@city,@state,@role_id)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@fname", user.F_Name);
            cmd.Parameters.AddWithValue("@lname", user.L_Name);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@contact", user.Contact);
            //cmd.Parameters.AddWithValue("@password", Encrypt(user.Password));
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@city", user.City);
            cmd.Parameters.AddWithValue("@state", user.State);
            cmd.Parameters.AddWithValue("@role_id", user.Role_Id);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public User UserLogin(User user)
        {
            User u = new User();
            string qry = "select * from users where email=@email and password=@password";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@password", user.Password);
            //cmd.Parameters.AddWithValue("@password", Decrypt(user.Password));
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    u.User_Id = Convert.ToInt32(dr["user_id"]);
                    u.Email = dr["email"].ToString();
                    u.F_Name = dr["fname"].ToString();
                    u.Role_Id = Convert.ToInt32(dr["role_id"]);
                }
                con.Close();
                return u;
            }
            else
            {
                return null;
            }
        }

        //public string Encrypt( string password)
        //{
        //    try
        //    {
        //        string ourText = password;
        //        string Return = null;
        //        string _key = "abcdefgh";
        //        string privatekey = "hgfedcba";
        //        byte[] privatekeyByte = { };
        //        privatekeyByte = Encoding.UTF8.GetBytes(privatekey);
        //        byte[] _keybyte = { };
        //        _keybyte = Encoding.UTF8.GetBytes(_key);
        //        byte[] inputtextbyteArray = System.Text.Encoding.UTF8.GetBytes(ourText);
        //        using (DESCryptoServiceProvider dsp = new DESCryptoServiceProvider())
        //        {
        //            var memstr = new MemoryStream();
        //            var crystr = new CryptoStream(memstr, dsp.CreateEncryptor(_keybyte, privatekeyByte), CryptoStreamMode.Write);
        //            crystr.Write(inputtextbyteArray, 0, inputtextbyteArray.Length);
        //            crystr.FlushFinalBlock();
        //            return Convert.ToBase64String(memstr.ToArray());
        //        }
        //        return Return;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        //public string Decrypt(string password)
        //{
        //    try
        //    {
        //        string ourText = password;
        //        string x = null;
        //        string _key = "abcdefgh";
        //        string privatekey = "hgfedcba";
        //        byte[] privatekeyByte = { };
        //        privatekeyByte = Encoding.UTF8.GetBytes(privatekey);
        //        byte[] _keybyte = { };
        //        _keybyte = Encoding.UTF8.GetBytes(_key);
        //        byte[] inputtextbyteArray = new byte[ourText.Replace(" ", "+").Length];
        //        //This technique reverses base64 encoding when it is received over the Internet.
        //        inputtextbyteArray = Convert.FromBase64String(ourText.Replace(" ", "+"));
        //        using (DESCryptoServiceProvider dEsp = new DESCryptoServiceProvider())
        //        {
        //            var memstr = new MemoryStream();
        //            var crystr = new CryptoStream(memstr, dEsp.CreateDecryptor(_keybyte, privatekeyByte), CryptoStreamMode.Write);
        //            crystr.Write(inputtextbyteArray, 0, inputtextbyteArray.Length);
        //            crystr.FlushFinalBlock();
        //            return Encoding.UTF8.GetString(memstr.ToArray());
        //        }
        //        return x;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
    }
}
