using Microsoft.Data.SqlClient;
using SalesCommissionAPI.Models;
using System.Data;

namespace SalesCommissionAPI.Repositories
{
	public class SalesmanRepository
	{
		private readonly string _connectionString;

		public SalesmanRepository(IConfiguration configuration)
		{
		
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		
		public List<Salesman> GetAllSalesmen()
		{
			List<Salesman> salesmen = new List<Salesman>();

			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand("SELECT * FROM Salesman", conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					salesmen.Add(new Salesman
					{
						Id = (int)reader["Id"],
						Name = reader["Name"].ToString(),
						Role = reader["Role"].ToString(),
						PreviousYearSales = (decimal)reader["PreviousYearSales"]
					});
				}
			}
			return salesmen;
		}

	
		public Salesman GetSalesmanById(int id)
		{
			Salesman salesman = null;

			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand("SELECT * FROM Salesman WHERE Id = @Id", conn);
				cmd.Parameters.AddWithValue("@Id", id);

				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				if (reader.Read())
				{
					salesman = new Salesman
					{
						Id = (int)reader["Id"],
						Name = reader["Name"].ToString(),
						Role = reader["Role"].ToString(),
						PreviousYearSales = (decimal)reader["PreviousYearSales"]
					};
				}
			}
			return salesman;
		}

		
		public void AddSalesman(Salesman salesman)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand("INSERT INTO Salesman (Name, Role, PreviousYearSales) VALUES (@Name, @Role, @PreviousYearSales)", conn);
				cmd.Parameters.AddWithValue("@Name", salesman.Name);
				cmd.Parameters.AddWithValue("@Role", salesman.Role);
				cmd.Parameters.AddWithValue("@PreviousYearSales", salesman.PreviousYearSales);

				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}

		
		public void UpdateSalesman(Salesman salesman)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand("UPDATE Salesman SET Name = @Name, Role = @Role, PreviousYearSales = @PreviousYearSales WHERE Id = @Id", conn);
				cmd.Parameters.AddWithValue("@Id", salesman.Id);
				cmd.Parameters.AddWithValue("@Name", salesman.Name);
				cmd.Parameters.AddWithValue("@Role", salesman.Role);
				cmd.Parameters.AddWithValue("@PreviousYearSales", salesman.PreviousYearSales);

				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}

		
		public void DeleteSalesman(int id)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand("DELETE FROM Salesman WHERE Id = @Id", conn);
				cmd.Parameters.AddWithValue("@Id", id);

				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}
	}
}
