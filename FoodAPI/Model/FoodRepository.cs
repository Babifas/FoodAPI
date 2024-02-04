using System.Data;
using System.Data.SqlClient;

namespace FoodAPI.Model
{
    public interface IFoodRepository
    {
        List<Foods> GetAllFoods();
        Foods GetFoodById(int id);
        void AddFood(Foods food);
        void UpdateFood(Foods food, int id);
        string DeleteFood(int id);
    }
    public class FoodRepository:IFoodRepository
    {
        private readonly string _connectionString;
      public FoodRepository(IConfiguration configuration) {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public List<Foods> GetAllFoods() 
        { 
        
         using(SqlConnection conn=new SqlConnection(_connectionString))
            {
               conn.Open();
                SqlCommand cmd = new SqlCommand("GET_FOODS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                List<Foods> foods = new List<Foods>();
                while (reader.Read())
                {
                    foods.Add(new Foods
                    {
                        FoodId = Convert.ToInt32(reader["FOOD_ID"]),
                        FoodName = reader["FOOD_NAME"].ToString(),
                        FoodTaste = reader["FOOD_TASTE"].ToString(),
                        FoodPrice= Convert.ToInt32(reader["FOOD_PRICE"])
                    }) ;
                }
                return foods ;
            }
         
        }
        public Foods GetFoodById(int id)
        {
            using(SqlConnection conn=new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GETFOOD_BYID", conn);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FOOD_ID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   return new Foods
                    {   
                        FoodId = Convert.ToInt32(reader["FOOD_ID"]),
                        FoodName = reader["FOOD_NAME"].ToString(),
                        FoodTaste = reader["FOOD_TASTE"].ToString(),
                        FoodPrice = Convert.ToInt32(reader["FOOD_PRICE"])
                    };
                }
                else
                {
                    return null;
                }
            }
        }
        public void AddFood(Foods food)
        {
            using(SqlConnection conn=new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("ADD_FOOD", conn);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FOOD_NAME", food.FoodName);
                cmd.Parameters.AddWithValue("@FOOD_TASTE", food.FoodTaste);
                cmd.Parameters.AddWithValue("@FOOD_PRICE",food.FoodPrice);
                cmd.ExecuteNonQuery();
                
            }
        }
        public void UpdateFood(Foods food, int id)
        {
          using(SqlConnection connection=new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE_FOOD", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FOOD_ID", id);
                cmd.Parameters.AddWithValue("@FOOD_NAME", food.FoodName);
                cmd.Parameters.AddWithValue("@FOOD_TASTE", food.FoodTaste);
                cmd.Parameters.AddWithValue("@FOOD_PRICE", food.FoodPrice);
                cmd.ExecuteNonQuery();
            }
        }
        public string DeleteFood(int id)
        {
          using(SqlConnection conn=new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE_FOOD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FOOD_ID", id);
                int rowsAffected=cmd.ExecuteNonQuery ();
                if(rowsAffected > 0)
                {
                    return "DELETED SUCCESSFULLY";
                }
                else
                {
                    return "FOOD NOT FOUND";
                }
            }
        }

    }
}
