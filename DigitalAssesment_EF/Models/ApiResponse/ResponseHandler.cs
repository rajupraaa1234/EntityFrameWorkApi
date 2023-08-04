using System;
namespace DigitalAssesment_EF.Models.ApiResponse
{
	public class ResponseHandler
	{
		public static ApiResponse GetExceptionResponse(Exception ex)
		{
			ApiResponse response = new ApiResponse();
			response.Code = "400";
			response.ResponseData = ex.Message;
			return response;
        }
		public static ApiResponse GetApiResponse(ResponseType type,object? contract)
		{
			ApiResponse response;
			response = new ApiResponse { ResponseData = contract };
			switch (type)
			{
				case ResponseType.Success:
					response.Code = "200";
					response.Message = "Success";
					break;
				case ResponseType.NotFound:
                    response.Code = "200";
                    response.Message = "Data Not Found";
                    break;
            }
			return response;
        }
	}
}

