using System.Reflection;

namespace Utils
{
    public class ObjectMapping
    {
        public static void UpdatePropertiesWithMatchingNames(object source, object target)
        {
            Type sourceType = source.GetType();
            Type targetType = target.GetType();

            PropertyInfo[] sourceProperties = sourceType.GetProperties();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                string propertyName = sourceProperty.Name;

                PropertyInfo? targetProperty = targetType.GetProperty(propertyName);

                if (targetProperty is not null)
                {
                    object? sourceValue = sourceProperty.GetValue(source);
                    object? targetValue = targetProperty.GetValue(target);

                    if (sourceValue is not null && !sourceValue.Equals(targetValue))
                        targetProperty.SetValue(target, sourceValue);
                }
            }
        }

    }
}