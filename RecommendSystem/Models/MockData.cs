namespace RecommendationSystem.Models
{
    public static class MockData
    {
        public static ResponseModel ResponseModel => new ResponseModel
        {
            Center_lat = 32.76785658F,
            Center_long = -117.1314657F,
            Profits = new DistanceProfits
            {
                Work_distance = 18.5F,
                Work_time = 34
            },
            Apartments = new Appartment[] {
                new Appartment {
                    Lat = 32.77827388F,
                    Long = -117.1180337F,
                    Address = "Mission Valley Fwy, San Diego, CA 92108, USA",
                    Image_url = "https://s3.amazonaws.com/retsly-importd-production/test_data/listings/10.jpg",
                    Area = 31.4F,
                    Price = 406100F,
                    Leasing_available  = true,
                    Distance_to_center = 0.01699819F,
                    Profits = null
                },
                new Appartment {
                    Lat = 32.75351568F,
                    Long = -117.152593F,
                    Address = "1240 Johnson Ave, San Diego, CA 92103, USA",
                    Image_url = "https://s3.amazonaws.com/retsly-importd-production/test_data/listings/02.jpg",
                    Area = 75.2F,
                    Price = 777644,
                    Leasing_available = false,
                    Distance_to_center = 0.02553476F,
                    Profits = null
                }
            }

        };
    
    }
}
