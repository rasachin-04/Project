namespace CarModelManagement.Repositories
{
	using System.Data.SqlClient;
	using System.Threading.Tasks;
	using System.Collections.Generic;
	using Microsoft.Extensions.Configuration;
	using CarModelManagement.Models;

	public class CarModelRepository
	{
		private readonly string _connectionString;

		public CarModelRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		
		public async Task<IEnumerable<CarModel>> GetAllCarModelsAsync()
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand("SELECT * FROM CarModels", conn);
				await conn.OpenAsync();
				SqlDataReader reader = await cmd.ExecuteReaderAsync();

				List<CarModel> carModels = new List<CarModel>();

				while (await reader.ReadAsync())
				{
					carModels.Add(new CarModel
					{
						Id = (int)reader["Id"],
						Brand = reader["Brand"].ToString(),
						Class = reader["Class"].ToString(),
						ModelName = reader["ModelName"].ToString(),
						ModelCode = reader["ModelCode"].ToString(),
						Description = reader["Description"].ToString(),
						Features = reader["Features"].ToString(),
						Price = (decimal)reader["Price"],
						DateOfManufacturing = (DateTime)reader["DateOfManufacturing"],
						Active = (bool)reader["Active"],
						SortOrder = (int)reader["SortOrder"],
						ModelImages = reader["ModelImages"].ToString().Split(',')
					});
				}
				return carModels;
			}
		}

		
		public async Task<int> CreateCarModelAsync(CarModel carModel)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand(
					"INSERT INTO CarModels (Brand, Class, ModelName, ModelCode, Description, Features, Price, DateOfManufacturing, Active, SortOrder, ModelImages) " +
					"VALUES (@Brand, @Class, @ModelName, @ModelCode, @Description, @Features, @Price, @DateOfManufacturing, @Active, @SortOrder, @ModelImages); " +
					"SELECT SCOPE_IDENTITY();", conn);

				cmd.Parameters.AddWithValue("@Brand", carModel.Brand);
				cmd.Parameters.AddWithValue("@Class", carModel.Class);
				cmd.Parameters.AddWithValue("@ModelName", carModel.ModelName);
				cmd.Parameters.AddWithValue("@ModelCode", carModel.ModelCode);
				cmd.Parameters.AddWithValue("@Description", carModel.Description);
				cmd.Parameters.AddWithValue("@Features", carModel.Features);
				cmd.Parameters.AddWithValue("@Price", carModel.Price);
				cmd.Parameters.AddWithValue("@DateOfManufacturing", carModel.DateOfManufacturing);
				cmd.Parameters.AddWithValue("@Active", carModel.Active);
				cmd.Parameters.AddWithValue("@SortOrder", carModel.SortOrder);
				cmd.Parameters.AddWithValue("@ModelImages", string.Join(",", carModel.ModelImages));

				await conn.OpenAsync();
				int newId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
				return newId;
			}
		}

		
		public async Task<bool> UpdateCarModelAsync(int id, CarModel carModel)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand(
					"UPDATE CarModels SET Brand = @Brand, Class = @Class, ModelName = @ModelName, ModelCode = @ModelCode, Description = @Description, Features = @Features, " +
					"Price = @Price, DateOfManufacturing = @DateOfManufacturing, Active = @Active, SortOrder = @SortOrder, ModelImages = @ModelImages WHERE Id = @Id", conn);

				cmd.Parameters.AddWithValue("@Id", id);
				cmd.Parameters.AddWithValue("@Brand", carModel.Brand);
				cmd.Parameters.AddWithValue("@Class", carModel.Class);
				cmd.Parameters.AddWithValue("@ModelName", carModel.ModelName);
				cmd.Parameters.AddWithValue("@ModelCode", carModel.ModelCode);
				cmd.Parameters.AddWithValue("@Description", carModel.Description);
				cmd.Parameters.AddWithValue("@Features", carModel.Features);
				cmd.Parameters.AddWithValue("@Price", carModel.Price);
				cmd.Parameters.AddWithValue("@DateOfManufacturing", carModel.DateOfManufacturing);
				cmd.Parameters.AddWithValue("@Active", carModel.Active);
				cmd.Parameters.AddWithValue("@SortOrder", carModel.SortOrder);
				cmd.Parameters.AddWithValue("@ModelImages", string.Join(",", carModel.ModelImages));

				await conn.OpenAsync();
				int rowsAffected = await cmd.ExecuteNonQueryAsync();
				return rowsAffected > 0;
			}
		}

	
		public async Task<bool> DeleteCarModelAsync(int id)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand("DELETE FROM CarModels WHERE Id = @Id", conn);
				cmd.Parameters.AddWithValue("@Id", id);

				await conn.OpenAsync();
				int rowsAffected = await cmd.ExecuteNonQueryAsync();
				return rowsAffected > 0;
			}
		}
	}


}
