﻿namespace HeroicallyRecipes.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/bundles/kendo")
                .Include("~/Scripts/kendoUI/jquery.min.js")
                .Include("~/Scripts/kendoUI/kendo.all.min.js")
                .Include("~/Scripts/kendoUI/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap.theme.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/kendo")
                .Include("~/Content/kendoUI/kendo.common-bootstrap.min.css")
                .Include("~/Content/kendoUI/kendo.silver.min.css"));
        }
    }
}
