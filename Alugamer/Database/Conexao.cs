using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Alugamer.Database
{
	public class Conexao
	{
		private SqlConnection _connck;
		private SqlTransaction _transaction;

		internal Conexao()
		{
			if (_connck == null)
			{
				//_connck = new SqlConnection("data source=localhost;initial catalog=alugamer;integrated security=true;");
				#if !(TRAVIS || TESTE)
				_connck = new SqlConnection("data source=alugamer.cam921hw53qq.us-east-2.rds.amazonaws.com;initial catalog=alugamer;User ID=admin;Password=dfOoEQOxSCbDTVgcAhk3");
				#endif
				#if (TRAVIS || TESTE)
				_connck = new SqlConnection("data source=alugamer.cam921hw53qq.us-east-2.rds.amazonaws.com;initial catalog=alugamer_testes;User ID=admin;Password=dfOoEQOxSCbDTVgcAhk3");
				#endif
			}
		}

		internal Conexao(string strConn)
		{
			if (_connck == null)
			{
				_connck = new SqlConnection(strConn);
			}
		}

		internal void open()
		{
			if (_connck.State == ConnectionState.Closed)
			{
				_connck.Open();
				using (SqlCommand cmd = new SqlCommand("SET DATEFORMAT mdy", _connck))
				{
					cmd.ExecuteNonQuery();
				}
			}
		}

		internal void close()
		{
			if (_connck.State.Equals(ConnectionState.Open))
				_connck.Close();
		}

		internal virtual void execute(string sql)
		{
			this.open();
			using (SqlCommand cmd = new SqlCommand(sql, _connck))
			{
				cmd.CommandTimeout = 0;
				cmd.ExecuteNonQuery();
			}
			this.close();
		}

		internal virtual int executeReturnRows(string sql)
		{
			int rows = 0;
			this.open();
			using (SqlCommand cmd = new SqlCommand(sql, _connck))
			{
				cmd.CommandTimeout = 0;
				rows = cmd.ExecuteNonQuery();
			}
			this.close();
			return rows;
		}

		internal virtual object scalar(string sql)
		{
			this.open();
			using (SqlCommand cmd = new SqlCommand(sql, _connck))
			{
				cmd.CommandTimeout = 0;
				object result = null;
				result = cmd.ExecuteScalar();
				this.close();
				return result;
			}
		}

		internal virtual DataTable dataTable(string sql)
		{
			this.open();
			using (SqlCommand cmd = new SqlCommand(sql, _connck))
			{
				cmd.CommandTimeout = 0;
				using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
				{
					DataTable tb = new DataTable("TEMP");
					//adapter.SelectCommand.Transaction = _transacao;
					adapter.Fill(tb);
					//_transacao.Commit();
					this.close();
					return tb;
				}
			}

		}

		internal virtual DataSet dataSet(string sql)
		{
			this.open();
			using (SqlCommand cmd = new SqlCommand(sql, _connck))
			{
				cmd.CommandTimeout = 0;
				using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
				{
					DataSet ds = new DataSet();
					try
					{
						//adapter.SelectCommand.Transaction = _transacao;
						adapter.SelectCommand.CommandTimeout = 0;
						adapter.Fill(ds);
						//_transacao.Commit();
					}
					catch (Exception)
					{
						//System.Web.HttpContext.Current.Response.Write(ex.Message);
						//_transacao.Rollback();
					}
					finally
					{
						this.close();
					}
					return ds;
				}
			}
		}

		internal virtual void fill(string sql, ref DataTable tb)
		{
			this.open();
			using (SqlCommand cmd = new SqlCommand(sql, _connck))
			{
				cmd.CommandTimeout = 0;
				using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
				{
					try
					{
						//adapter.SelectCommand.Transaction = _transacao;
						adapter.SelectCommand.CommandTimeout = 0;
						adapter.Fill(tb);
						//_transacao.Commit();
					}
					catch (Exception)
					{
						//System.Web.HttpContext.Current.Response.Write(ex.Message);
						//_transacao.Rollback();
					}
					finally
					{
						this.close();
					}
				}
			}
		}

		internal virtual DataSet dataSet(string sql, string tabela)
		{
			this.open();
			using (SqlCommand cmd = new SqlCommand(sql, _connck))
			{
				cmd.CommandTimeout = 0;
				using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
				{
					DataSet ds = new DataSet();
					try
					{
						//adapter.SelectCommand.Transaction = _transacao;
						adapter.SelectCommand.CommandTimeout = 0;
						adapter.Fill(ds, tabela);
						//_transacao.Commit();
					}
					catch (Exception)
					{
						//System.Web.HttpContext.Current.Response.Write(ex.Message);
						//_transacao.Rollback();
					}
					finally
					{
						this.close();
					}
					return ds;
				}
			}
		}

		internal virtual List<Hashtable> reader(string sql)
		{
			this.open();
			using (SqlCommand cmd = new SqlCommand(sql, _connck))
			{
				cmd.CommandTimeout = 0;
				List<Hashtable> arr = new List<Hashtable>();
				try
				{
					//cmd.Transaction = _transacao;
					SqlDataReader leitor = cmd.ExecuteReader();
					int colunas = leitor.FieldCount;

					while (leitor.Read())
					{
						Hashtable ht = new Hashtable();
						for (int i = 0; i < colunas; i++)
						{
							ht.Add(leitor.GetName(i), leitor[leitor.GetName(i)]);
						}
						arr.Add(ht);
					}
					leitor.Close();
					//_transacao.Commit();
				}
				catch (Exception)
				{
					//System.Web.HttpContext.Current.Response.Write(ex.Message);
					//_transacao.Rollback();
				}
				finally
				{
					this.close();
				}
				return arr;
			}
		}
		internal virtual SqlDataReader reader_list(string sql)
		{

			this.open();
			using (SqlCommand cmd = new SqlCommand(sql, _connck))
			{
				cmd.CommandTimeout = 0;
				SqlDataReader rds = cmd.ExecuteReader();
				this.close();
				return rds;
			}
		}

		internal virtual void open_transaction()
		{
			this.open();

			if (_transaction == null)
				_transaction = _connck.BeginTransaction(IsolationLevel.ReadCommitted);

		}

		internal virtual void close_transaction()
		{

			if (_transaction == null)
				return;

			try
			{
				_transaction.Commit();
			}
			catch
			{
				try
				{
					_transaction.Rollback();
				}
				catch
				{
					_transaction.Dispose();
					_transaction = null;
					this.close();
					throw new Exception();
				}
			}

			_transaction.Dispose();
			_transaction = null;
			this.close();


		}

		internal virtual object scalar_transaction(string sql)
		{
			this.open_transaction();
			using (SqlCommand cmd = new SqlCommand(sql, _connck, _transaction))
			{
				cmd.CommandTimeout = 0;
				object result = null;
				result = cmd.ExecuteScalar();
				return result;
			}
		}

		internal virtual void execute_transaction(string sql)
		{
			try
			{
				this.open_transaction();
				using (SqlCommand cmd = new SqlCommand(sql, _connck, _transaction))
				{
					cmd.CommandTimeout = 0;
					cmd.ExecuteNonQuery();
				}
			}
			catch
			{
				this.rollback_transaction();
				throw new Exception();
			}

		}
		internal virtual void rollback_transaction()
		{
			try
			{
				if (_transaction != null)
					_transaction.Rollback();
			}
			catch
			{
				this.close_transaction();
				throw new Exception();
			}
		}

	}
}
