namespace Satrack.Application.Common
{
	public static class FluentValidationExtension
	{
        public static bool BeAValidDate(this DateTime date) => date != default;
        public static bool BeAValidGuid(this Guid guid) => guid != Guid.Empty;
    }
}

