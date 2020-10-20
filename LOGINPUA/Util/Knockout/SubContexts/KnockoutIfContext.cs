﻿using System.Web.Mvc;

namespace LOGINPUA.Util.Knockout
{
  public class KnockoutIfContext<TModel> : KnockoutCommonRegionContext<TModel>
  {
    public KnockoutIfContext(ViewContext viewContext, string expression) : base(viewContext, expression)
    {
    }

    protected override string Keyword
    {
      get
      {
        return "if";
      }
    }
  }
}