﻿!function (a, b, c, d) {
    var e, f = document,
        g = f.getElementsByTagName("SCRIPT"),
        h = g[g.length - 1].previousElementSibling,
        i = f.defaultView && f.defaultView.getComputedStyle
            ? f.defaultView.getComputedStyle(h)
            : h.currentStyle;
    if (i && i[a] !== b)
        for (e = 0; e < c.length; e++)
            f.write('<link href="' + c[e] + '" ' + d + "/>")
}("visibility", "hidden", ["/lib/element-plus/theme-chalk/index.min.css"], "rel=\u0022stylesheet\u0022 ");