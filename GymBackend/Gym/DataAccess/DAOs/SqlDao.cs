﻿using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccess.DAOs;

public class SqlDao

    //Clase que se encarga de la comunicaccion a lab ase de datos, solo ejecuta proceddures.
    //Utiliza el singleton (Unica instancia)
{
    //SingleTon
    //Crear instancia privada
    private static SqlDao _instance;

    //Guarda la ruta para llegar al servidor DB
    private readonly string _connectionString;

    //Redefinir Constructor como uno privado
    private SqlDao()
    {
        _connectionString = "Data Source=ANDRESPC;Initial Catalog=sc_db;Integrated Security=True;Encrypt=False";
    }

    //Definir metodo de instancia del sqldao
    public static SqlDao GetInstance()
    {
        if (_instance == null) _instance = new SqlDao();
        return _instance;
    }

    //No regresa retorno exepto de excepcion, solo ejecuta
    public void ExecuteProcedure(SqlOperation sqlOperation)
    {
        //Definir instancia
        using (var conn = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
                   {
                       CommandType = CommandType.StoredProcedure
                   })
            {
                //Agregar params
                foreach (var param in sqlOperation.Parameters) command.Parameters.Add(param);

                //Ejecutar contra base de datos
                conn.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation sqlOperation)
    {
        var listResults = new List<Dictionary<string, object>>();
        //Definir instancia
        using (var conn = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
                   {
                       CommandType = CommandType.StoredProcedure
                   })
            {
                //Agregar params
                foreach (var param in sqlOperation.Parameters) command.Parameters.Add(param);

                //Ejecutar contra base de datos
                conn.Open();
                //Aqui cambia al anterior 
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var rowDictionary = new Dictionary<string, object>();
                        for (var index = 0; index < reader.FieldCount; ++index)
                        {
                            var key = reader.GetName(index);
                            var value = reader.GetValue(index);
                            rowDictionary[key] = value;
                        }

                        listResults.Add(rowDictionary);
                    }
            }
        }

        return listResults;
    }
}