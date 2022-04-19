using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace _19042022
{
    class PostData
    {
        public void AddPost(string title, string content)
        {
            Post post = new Post();
            using (SqlConnection con = new SqlConnection(Sql.ConnectionString))
            {
                con.Open();

                string query = "INSERT INTO Post(Title,Content) VALUES (@title,@content)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("title", post.Title);
                cmd.Parameters.AddWithValue("content", post.Content);
            }
        }

        public List<Post> GetAllPosts()
        {
            List<Post> posts = new List<Post>();
            using (SqlConnection con = new SqlConnection(Sql.ConnectionString))
            {
                con.Open();

                string query = "SELECT *FROM Post";
                SqlCommand cmd = new SqlCommand(query, con);
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        Post post = new Post
                        {
                            Id = rd.GetInt32(0),
                            Title = rd.GetString(1),
                            Content = rd.GetString(2)
                        };
                        posts.Add(post);
                    }
                    return posts;
                }

            }
        }
        public void DeletePost(int id)
        {
            using (SqlConnection con = new SqlConnection(Sql.ConnectionString))
            {
                con.Open();

                string query = "DELETE *FROM Post AS P WHERE P.Id=" + id;
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }
        public Post GetPostById(string id)
        {
            Post post = new Post();
            using (SqlConnection con = new SqlConnection(Sql.ConnectionString))
            {
                con.Open();

                string query = "SELECT * FROM Post WHERE Id=@id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("id", id);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            post.Id = dr.GetInt32(0);
                            post.Title = dr.GetString(1);
                            post.Content = dr.GetString(2);
                        }
                        return post;
                    }
                    else return null;

                }
            }
        }
        public void EditPostTitle(string id)
        {
            
            using (SqlConnection con = new SqlConnection(Sql.ConnectionString))
            {
                con.Open();

                string query = "UPDATE Post SET Title='value1' WHERE Id=@id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("id", id);
                
               
            }
        }

    }
}
