using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Documentation;

public class Specifier<T> : ISpecifier
{
    public string GetApiDescription()
    {
        var str = typeof(T).GetCustomAttribute<ApiDescriptionAttribute>();
        if (str != null)
            return str.Description;
        return null;
    }

    public string[] GetApiMethodNames()
    {
        var methods = typeof(VkApi).GetMethods()
            .Where(z => z.GetCustomAttributes(typeof(ApiMethodAttribute)).Any())
            .Select(x => x.Name);
        return methods.ToArray();
    }

    public string GetApiMethodDescription(string methodName)
    {
        var method = typeof(VkApi).GetMethods()
            .Where(z => z.Name == methodName)
            .Select(x => x.GetCustomAttribute<ApiDescriptionAttribute>())
            .FirstOrDefault();
        if (method != null)
            return method.Description;
        return null;
    }

    public string[] GetApiMethodParamNames(string methodName)
    {
        var parametrsName = typeof(VkApi).GetMethods()
            .Where(z => z.Name == methodName)
            .SelectMany(x => x.GetParameters()
            .Select(x => x.Name))
            .ToArray();
        if (parametrsName != null)
            return parametrsName;
        return null;
    }

    public string GetApiMethodParamDescription(string methodName, string paramName)
    {
        var paramDescription = typeof(VkApi).GetMethods()
            .Where(z => z.Name == methodName)
            .SelectMany(x => x.GetParameters()
            .Where(x => x.Name == paramName))
            .FirstOrDefault();
        if (paramDescription != null
            && paramDescription.GetCustomAttribute<ApiDescriptionAttribute>() != null)
            return paramDescription.GetCustomAttribute<ApiDescriptionAttribute>()
                .Description;
        return null;
    }

    public (int?, int?) GetApiMethodParamMinMax(string methodName, string paramName)
    {
        var paramInfo = typeof(VkApi).GetMethods()
            .Where(z => z.Name == methodName)
            .SelectMany(x => x.GetParameters()
            .Where(x => x.Name == paramName))
            .FirstOrDefault();
        if (paramInfo != null
            && paramInfo.GetCustomAttribute<ApiIntValidationAttribute>() != null)
            return (paramInfo.GetCustomAttribute<ApiIntValidationAttribute>().MinValue,
            paramInfo.GetCustomAttribute<ApiIntValidationAttribute>().MaxValue);
        return default;
    }

    public bool GetApiMethodParamRequire(string methodName, string paramName)
    {
        var paramInfo = typeof(VkApi).GetMethods()
            .Where(z => z.Name == methodName)
            .SelectMany(x => x.GetParameters()
            .Where(x => x.Name == paramName))
            .FirstOrDefault();
        if (paramInfo != null
            && paramInfo.GetCustomAttribute<ApiRequiredAttribute>() != null)
            return paramInfo.GetCustomAttribute<ApiRequiredAttribute>().Required;
        return false;
    }

    public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
    {
        var ans = new ApiParamDescription();

        var description = GetApiMethodParamDescription(methodName, paramName);
        var (min, max) = GetApiMethodParamMinMax(methodName, paramName);
        var require = GetApiMethodParamRequire(methodName, paramName);

        if (description != null)
            ans.ParamDescription = new CommonDescription(paramName, description);
        else
            ans.ParamDescription = new CommonDescription(paramName);

        if ((min, max) != default)
            (ans.MinValue, ans.MaxValue) = (min, max);

        ans.Required = require;
        return ans;
    }

    public bool IsApiMethod(string methodName)
    {
        var method = typeof(VkApi).GetMethods()
            .Where(z => z.Name == methodName)
            .Select(x => x.GetCustomAttribute<ApiMethodAttribute>())
            .FirstOrDefault();
        return method != null;
    }

    public ApiParamDescription GetApiReturnDescription(string methodName)
    {
        var res = new ApiParamDescription
        { ParamDescription = new CommonDescription() };

        var methodReturn = typeof(VkApi).GetMethods()
            .Where(z => z.Name == methodName)
            .FirstOrDefault();

        if (methodReturn.ReturnType.Name == "Void")
            return null;
        var returnRequired = methodReturn.ReturnParameter
            .GetCustomAttributes<ApiRequiredAttribute>()
            .FirstOrDefault();
        var returnValidationd = methodReturn.ReturnParameter
            .GetCustomAttributes<ApiIntValidationAttribute>()
            .FirstOrDefault();
        if (returnRequired != null)
            res.Required = returnRequired.Required;
        if (returnValidationd != null)
        {
            res.MinValue = returnValidationd.MinValue;
            res.MaxValue = returnValidationd.MaxValue;
        }
        return res;
    }

    public ApiMethodDescription GetApiMethodFullDescription(string methodName)
    {
        var ans = new ApiMethodDescription();
        if (!IsApiMethod(methodName))
            return null;

        var methodDescription = GetApiMethodDescription(methodName);
        ans.MethodDescription = new CommonDescription(methodName, methodDescription);

        var paramNames = GetApiMethodParamNames(methodName);
        var list = new List<ApiParamDescription>();
        foreach (var name in paramNames)
            list.Add(GetApiMethodParamFullDescription(methodName, name));
        ans.ParamDescriptions = list.ToArray();
        ans.ReturnDescription = GetApiReturnDescription(methodName);

        return ans;
    }
}