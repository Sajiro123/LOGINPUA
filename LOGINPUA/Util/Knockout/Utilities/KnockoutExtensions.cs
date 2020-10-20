﻿using System.Web.Mvc;

namespace LOGINPUA.Util.Knockout
{
    public static class KnockoutExtensions
    {
        public static KnockoutContext<TModel> CreateKnockoutContext<TModel>(this HtmlHelper<TModel> helper)
        {
            return new KnockoutContext<TModel>(helper.ViewContext);
        }

    }
}
