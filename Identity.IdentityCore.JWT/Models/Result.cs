namespace Identity.IdentityCore.JWT.Models
{
    public class Result
    {
        public bool Success { get; }
        public IEnumerable<string> Errors { get; }
        public object? Value { get; } // Optional data associated with the result

        // Constructor for success result
        public Result(object? value = null)
        {
            Success = true;
            Value = value;
            Errors = Enumerable.Empty<string>();
        }

        // Constructor for failure result
        public Result(IEnumerable<string> errors)
        {
            Success = false;
            Errors = errors;
            Value = null;
        }

        // Static method to create success result
        public static Result Succesed(object? value = null) => new Result(value);

        // Static method to create failure result
        public static Result Failure(IEnumerable<string> errors) => new Result(errors);
    }
}
