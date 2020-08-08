using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace WpfApplication1
{
    class CCasaSQL
    {
        public string           m_sConnetionString;
        public SqlConnection    m_connectionSql;
        public bool             m_bConnectionOpen;
        public SqlDataReader    m_readerSql;
        public ConnectionState  m_connectionState;
        
        // constructor
        //
        public CCasaSQL( string sTableName )
        {
            m_sConnetionString  = "Server=localhost\\SQLEXPRESS;Initial Catalog="+sTableName+";Trusted_Connection=true";
            m_connectionSql     = new SqlConnection(m_sConnetionString);
            m_bConnectionOpen   = true;

            try
            {
                m_connectionSql.Open();
            }

            catch (SqlException)
            {
                m_bConnectionOpen   = false;
                m_connectionState  = m_connectionSql.State;
            }
        }

        
        // open forward-ready only rowset 
        // returns if number of rows returned > 0; false if not
        //
        public bool OpenRowset(string sSql)
        {
            SqlCommand cmd = new SqlCommand(sSql, m_connectionSql);
            m_readerSql = cmd.ExecuteReader();

            return (m_readerSql.HasRows);
        }

        // for sql statements not requiring rowset: UPDATE, INSERT, DELETE, etc..
        //  - returns number of rows affected;
        //
        public int ExecuteNoRowset(string sSql)
        {
            SqlCommand cmd = new SqlCommand(sSql, m_connectionSql);
            return cmd.ExecuteNonQuery();
        }

        public string GetFieldValue(string sField)
        {
            return m_readerSql.GetString(m_readerSql.GetOrdinal(sField));
        }
     }
}
