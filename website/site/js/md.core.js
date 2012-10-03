/* 
 * Metro Dynamis Core Web
 * Version: 1.0.6
 * Author: Jose E. Gonzalez Modecir 
 * License: https://metrodynamis.com/product/metrodynamis-core-web/license/
 * http://metrodynamis.com
 * 
 */

(function( MD, $, undefined ) {
    MD.version = "1.0";

    //Public namespaces
    MD.Helpers = { };
    
    MD.UI = { };

    MD.UI.Nav = {
        fixPositions: function () {
            $('.md-nav:visible').each(function () {
                var el = $(this).contents().find('li');
                var eo = new Array();
                for (var i = 0; i < el.length; i++) eo[i] = $(el[i]).position();
                for (var i = 0; i < el.length; i++) {
                    $(el[i]).css({ position: 'absolute', top: eo[i].top, left: eo[i].left });
                }
            });

        }

    };

    MD.UI.Viewport = {
        setAllHeight : function(){
            $('.md-viewport-vertical:visible').each(function(){
                var vo = $(this).offset();
                $(this).height($(window).height() - vo.top);
                $(this).children('.md-scrollable').height(MD.UI.Calc.elementsHeight($('.md-scrollable')) + 40);
            });
        },
    	
        setAllWidth: function () {
            $('.md-viewport-horizontal:visible').each(function(){
                var vo = $(this).offset();
                $(this).height($(window).height() - vo.top);
            });
            $('.md-auto-resize:visible').each(function () {
                $(this).width(MD.UI.Calc.elementsWidth(this));
            });
            $('.md-auto-max-width:visible').each(function () {
                var mw = 0;
                $(this).children().each(function () {
                    var tw = $(this).outerWidth(true);
                    if (tw > mw) mw = tw;
                });
                $(this).width(mw);
            });
            $('.md-viewport-horizontal:visible > .md-scrollable').each(function () {
                $(this).width(MD.UI.Calc.elementsWidth(this) + 120);
            });
            $('.md-viewport-horizontal:visible').mousewheel(function(event, delta) {
                this.scrollLeft -= (delta * 40);
                event.preventDefault();
            });
        },
		
    };

    MD.UI.List = {
        autoArrangeTiles: function () {
            $('.md-auto-arrange-tiles:visible').each(function () {
                if ($(this).height == 0) return;
                var eOffset = $(this).offset();
                var hParent = Math.floor($(window).height() - eOffset.top - 40);
                $(this).height(hParent);
                var hItem = Math.floor($(this).find('.md-list-item:first-child').outerHeight(true));
                var wItem = Math.floor($(this).find('.md-list-item:first-child').outerWidth(true));
                var mRows = Math.floor(hParent / hItem);
                var mColums = Math.ceil($(this).children().length / mRows);
                var sWidth = Math.floor(mColums * wItem / 10) * 10 + 10;
                $(this).width(sWidth);
            });

        }

    };

    MD.UI.Columns = {
        autoArrangeColumns: function () {
            $('.md-auto-columns:visible').each(function () {
                if ($(this).children().hasClass('md-column')) return;
                var eOffset = $(this).offset();
                var hParent = Math.floor($(window).height() - eOffset.top - 40);
                var el = $(this).children();
                $(this).empty();
                var j = 0;
                var cc = new Array();
                cc[j] = $('<div class="md-column"></div>');
                $(cc[j]).appendTo(this);
                var i = 0;
                while (i <= $(el).length) {
                    $(el[i]).appendTo(cc[j]);
                    if ($(cc[j]).outerHeight(true) > hParent) {
                        $(el[i]).remove();
                        j++;
                        cc[j] = $('<div class="md-column"></div>');
                        $(cc[j]).appendTo(this);
                        $(el[i]).appendTo(cc[j]);
                    }
                    i++;
                }
                $(this).width(MD.UI.Calc.elementsWidth(this));
            });

        }

    };

    MD.UI.Calc = {
    	elementsWidth : function(element) {
    		var m = 0;
    		$(element).children().each(function(){
    			m = m + $(this).outerWidth(true);
    		});
    		return m;
    	},
    	
		elementsHeight : function(element) {
			var m = 0;
			$(element).children().each(function(){
				m = m + $(this).outerHeight(true);
			});
			return m;
		}
    };
    
    MD.UI.recalculateAll = function () {
        var fns = [MD.UI.Columns.autoArrangeColumns, MD.UI.List.autoArrangeTiles, MD.UI.Viewport.setAllWidth, MD.UI.Viewport.setAllHeight];
        for (i = 0; i < fns.length; i++) { fns[i](); }
    };

    MD.Initialize = function () {
        MD.UI.recalculateAll();
        MD.UI.Nav.fixPositions();
    };
    
}(window.MD = window.MD || {}, jQuery));