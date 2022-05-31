using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mime;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using MonitorMarkets.Application.Objects.Data.Attributes;
using MonitorMarkets.Application.Objects.Data.Enums;

namespace MonitorMarkets.Vizualizer.View
{
    public partial class TradingWindow : Window
    {
        MarketsEnum status;
        public TradingWindow()
        {
            InitializeComponent();
        }

        public void Box_OnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            Enum.TryParse<MarketsEnum>(SelectedMarket.SelectedValue.ToString(), out status);
        }
        
        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
        ///
        
        /*public static T GetAttributeOfType<T>(this Enum enumVal) where T:Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }*/
    }
}