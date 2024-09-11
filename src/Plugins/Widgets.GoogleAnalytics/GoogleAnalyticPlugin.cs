using Grand.Business.Core.Interfaces.Common.Configuration;
using Grand.Business.Core.Interfaces.Common.Localization;
using Grand.Infrastructure.Plugins;

namespace Widgets.GoogleAnalytics;

/// <summary>
///     Live person provider
/// </summary>
public class GoogleAnalyticPlugin(
    ISettingService settingService,
    IPluginTranslateResource pluginTranslateResource)
    : BasePlugin, IPlugin
{

    /// <summary>
    ///     Gets a configuration page URL
    /// </summary>
    public override string ConfigurationUrl()
    {
        return GoogleAnalyticDefaults.ConfigurationUrl;
    }

    /// <summary>
    ///     Install plugin
    /// </summary>
    public override async Task Install()
    {
        var settings = new GoogleAnalyticsEcommerceSettings {
            GoogleId = "000000000",
            TrackingScript = @"<!-- Google tag (gtag.js) -->
                    <script async src='https://www.googletagmanager.com/gtag/js?id={GOOGLEID}'></script>
                    <script>
                        window.dataLayer = window.dataLayer || [];
                        function gtag(){dataLayer.push(arguments);}
                        gtag('js', new Date());
                        gtag('config', '{GOOGLEID}');
                        {ECOMMERCE}
                    </script>
                ",
            EcommerceScript =
                "gtag('event', 'purchase', {transaction_id: '{ORDERID}', value: {TOTAL}, tax: {TAX}, shipping: {SHIP}, currency: '{CURRENCY}', city: '{CITY}', state: '{STATEPROVINCE}', country: '{COUNTRY}', items: [{DETAILS}]});",
            EcommerceDetailScript =
                "{ item_id: '{PRODUCTID}', item_name: '{PRODUCTNAME}', item_category: '{CATEGORYNAME}', price: {UNITPRICE}, quantity: {QUANTITY} }, ",
            ConsentName = "Google Analytics",
            ConsentDescription = "Allows us to analyse the statistics of visits to our website."
        };
        await settingService.SaveSetting(settings);

        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.FriendlyName", "Google Analytics");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.GoogleId", "ID");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.GoogleId.Hint", "Enter Google Analytics ID.");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.TrackingScript", "Tracking code with {ECOMMERCE} line");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.TrackingScript.Hint",
            "Paste the tracking code generated by Google Analytics here. {GOOGLEID} and {ECOMMERCE} will be dynamically replaced.");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.EcommerceScript", "Tracking code for {ECOMMERCE} part, with {DETAILS} line");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.EcommerceScript.Hint",
            "Paste the tracking code generated by Google analytics here. {ORDERID}, {SITE}, {TOTAL}, {TAX}, {SHIP}, {CITY}, {STATEPROVINCE}, {COUNTRY}, {DETAILS} will be dynamically replaced.");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.EcommerceDetailScript", "Tracking code for {DETAILS} part");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.EcommerceDetailScript.Hint",
            "Paste the tracking code generated by Google analytics here. {ORDERID}, {PRODUCTSKU}, {PRODUCTNAME}, {CATEGORYNAME}, {UNITPRICE}, {QUANTITY} will be dynamically replaced.");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.IncludingTax", "Include tax");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.IncludingTax.Hint",
            "Check to include tax when generating tracking code for {ECOMMERCE} part.");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.AllowToDisableConsentCookie", "Allow disabling consent cookie");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.AllowToDisableConsentCookie.Hint",
            "Get or set the value to disable consent cookie");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.ConsentDefaultState", "Consent default state");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.ConsentDefaultState.Hint", "Get or set the value to consent default state");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.ConsentName", "Consent cookie name");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.ConsentName.Hint", "Get or set the value to consent cookie name");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.ConsentDescription", "Consent cookie description");
        await pluginTranslateResource.AddOrUpdatePluginTranslateResource("Widgets.GoogleAnalytics.ConsentDescription.Hint", "Get or set the value to consent cookie description");

        await base.Install();
    }

    /// <summary>
    ///     Uninstall plugin
    /// </summary>
    public override async Task Uninstall()
    {
        //settings
        await settingService.DeleteSetting<GoogleAnalyticsEcommerceSettings>();

        //locales
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.GoogleId");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.GoogleId.Hint");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.TrackingScript");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.TrackingScript.Hint");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.EcommerceScript");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.EcommerceScript.Hint");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.EcommerceDetailScript");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.EcommerceDetailScript.Hint");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.IncludingTax");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.IncludingTax.Hint");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.AllowToDisableConsentCookie");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.AllowToDisableConsentCookie.Hint");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.ConsentDefaultState");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.ConsentDefaultState.Hint");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.ConsentName");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.ConsentName.Hint");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.ConsentDescription");
        await pluginTranslateResource.DeletePluginTranslationResource("Widgets.GoogleAnalytics.ConsentDescription.Hint");

        await base.Uninstall();
    }
}