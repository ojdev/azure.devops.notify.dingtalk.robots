using azure.devops.notify.dingtalk.robots.AzureDevOps;
using Newtonsoft.Json;

namespace azure.devops.notify.dingtalk.robots.Infrastructure
{
    public static class Extensions
    {
        public static ValueChangeModel ToValueChangeModel(this object source)
        {
            return JsonConvert.DeserializeObject<ValueChangeModel>(JsonConvert.SerializeObject(source));
        }
    }
}
