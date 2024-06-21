namespace WebApplicationMVC.Models
{
    public class BaseResponseModel
    {
        private string _ResultMessage = string.Empty;
        private bool _Success = false;

        public bool Success
        {
            get
            {
                return _Success;
            }
            set
            {
                _Success = value;
            }
        }

        public string ResultMessage
        {
            get
            {
                return _ResultMessage;
            }
            set
            {
                _ResultMessage = value;
            }
        }


    }
    [Serializable]
    public class BaseResponseModel<T> : BaseResponseModel
    {
        private T _GenericValue = default(T);
        public T Value
        {
            get
            {
                return _GenericValue;
            }
            set
            {
                _GenericValue = value;
            }
        }
    }
    public class BaseResponse
    {
        public string Value { get; set; }
    }
}
