﻿using Oracle.ManagedDataAccess.Client;
using Oracle.DataAccess.Types;
using ProjectBase.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Database
{
    //vyigity
    public class OracleManagedQueryGenerator : IQueryGenerator
    {
        public String TableName { get; set; }
        List<OracleParameter> DataParameters;
        List<OracleParameter> FilterParameters;

        bool isFilled = false;

        public string SelectText { get; set; }
        public string FilterText { get; set; }
        public string SelectTail { get; set; }
        public string ProcedureName { get; set; }

        OracleCommand command = new OracleCommand();

        public void AddFilterParameter(string parameterName, object value)
        {
            FilterParameters.Add(new OracleParameter(parameterName, value));
        }

        public void AddDataParameter(string parameterName, object value)
        {
            DataParameters.Add(new OracleParameter(parameterName, value));
        }

        public void AddDataParameter(string parameterName, object value, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(parameterName, value);
            param.Direction = direction;
            DataParameters.Add(param);
        }

        public void AddDataParameter(string parameterName, object value, int size, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            DataParameters.Add(param);
        }

        public void AddDataParameter(string parameterName, object dbBaseDbType, object value, ParameterDirection direction)
        {
            DataParameters.Add(new OracleParameter(parameterName, (OracleDbType)dbBaseDbType, value, direction));
        }

        public void AddDataParameter(string parameterName, object dbBaseDbType, object value, int size, ParameterDirection direction)
        {
            DataParameters.Add(new OracleParameter(parameterName, (OracleDbType)dbBaseDbType, size, value, direction));
        }

        public void AddDataParameter(string parameterName, DbType dbType, object value, int size, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.DbType = dbType;
            DataParameters.Add(param);
        }

        public OracleManagedQueryGenerator()
        {
            DataParameters = new List<OracleParameter>();
            FilterParameters = new List<OracleParameter>();
        }

        public object GetParameterValue(string parameterName)
        {
            foreach (OracleParameter item in command.Parameters)
            {
                if (item.ParameterName == parameterName)
                {
                    return item.Value;
                }
            }

            throw new KeyNotFoundException("Belirtilen anahtar değerine sahip bir parametre bulunamadı");
        }

        public IDbCommand GetInsertCommand()
        {
            if (!isFilled)
            {
                StringBuilder bString = new StringBuilder("INSERT INTO ");
                StringBuilder dString = new StringBuilder("(");
                StringBuilder vString = new StringBuilder("(");

                foreach (OracleParameter param in DataParameters)
                {
                    dString.Append(param.ParameterName);
                    dString.Append(",");

                    vString.Append(":");
                    vString.Append(param.ParameterName);
                    vString.Append(",");

                    command.Parameters.Add(param);
                }

                dString.Remove(dString.Length - 1, 1);
                vString.Remove(vString.Length - 1, 1);

                dString.Append(")");
                vString.Append(")");

                bString.Append(TableName);
                bString.Append(dString.ToString());

                bString.Append(" VALUES ");
                bString.Append(vString);

                command.CommandText = bString.ToString();
            }

            isFilled = true;

            return command;
        }

        public IDbCommand GetUpdateCommand()
        {
            if (!isFilled)
            {
                StringBuilder bString = new StringBuilder("UPDATE ");
                bString.Append(TableName);
                bString.Append(" SET ");

                foreach (OracleParameter param in DataParameters)
                {
                    bString.Append(param.ParameterName);
                    bString.Append("=:");
                    bString.Append(param.ParameterName);
                    bString.Append(",");

                    command.Parameters.Add(param);
                }

                bString.Remove(bString.Length - 1, 1);

                if (FilterText != null)
                {
                    bString.Append(" ");
                    FilterText.Replace("@", ":");
                    bString.Append(FilterText);
                }

                foreach (OracleParameter param in FilterParameters)
                {
                    command.Parameters.Add(param);
                }

                command.CommandText = bString.ToString();
            }

            isFilled = true;

            return command;
        }

        public IDbCommand GetSelectCommandBasic()
        {
            if (!isFilled)
            {
                StringBuilder bString = new StringBuilder(SelectText);

                if (FilterText != null)
                {
                    bString.Append(" ");

                    FilterText = FilterText.Replace("@", ":");
                    bString.Append(FilterText);
                }

                foreach (OracleParameter param in FilterParameters)
                {
                    command.Parameters.Add(param);
                }

                bString.Append(" ");

                if (SelectTail != null)
                    bString.Append(SelectTail);

                command.CommandText = bString.ToString();
            }

            isFilled = true;

            return command;
        }

        public IDbCommand GetProcedure()
        {
            if (!isFilled)
            {
                foreach (OracleParameter param in DataParameters)
                {
                    command.Parameters.Add(param);
                }

                command.CommandText = ProcedureName;
                command.CommandType = System.Data.CommandType.StoredProcedure;
            }

            isFilled = true;

            return command;
        }

        public void Clear()
        {
            if (DataParameters != null)
                DataParameters.Clear();
            if (FilterParameters != null)
                FilterParameters.Clear();

            TableName = null;
            SelectText = null;
            FilterText = null;
            SelectTail = null;
            ProcedureName = null;
            command = new OracleCommand();
            isFilled = false;
        }
    }
}
