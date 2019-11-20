
(function ($) {

	$.DynamicList = function (element, item_typename, model_prefix, options) {

		var DeleteItemClicked = function (e) {
			if (plugin.settings.OnBeforeItemDelete()) {
				$(this).closest(".DynamicList-Row").remove();
			}
		}

		var AddItemClicked = function (e) {
			if (!plugin.settings.OnBeforeItemAdd())
				return;
			var indexes = plugin.$element.find("input[name=\"" + plugin.model_prefix + (plugin.model_prefix.length > 0 ? "." : "") + "Index\"]").map(function () { return $(this).val(); }).get() || [];
			var item_index = 0;
			if (indexes.length != 0)
				item_index = Math.max.apply(Math, indexes) + 1;
			$.get(
				encodeURI(plugin.settings.AddItemUrl
					.replace("__item_type_fullname__", plugin.item_typename)
					.replace("__item_index__", item_index)
					.replace("__model_prefix__", plugin.model_prefix)),
				null,
				function (data) {
					plugin.$element.find(".DynamicList-Rows").append(data);
					plugin.$element.closest("form").removeData("validator");
					plugin.$element.closest("form").removeData("unobtrusiveValidation");
					$.validator.unobtrusive.parse(plugin.$element.closest("form"));
					plugin.$element.find(".DynamicList-RemoveItemButton").last().click(DeleteItemClicked);
				}
			);
		}

		var plugin = this;
		plugin.settings = {}
		plugin.item_typename = item_typename;
		plugin.model_prefix = model_prefix;
		plugin.$element = $(element);

		{
			plugin.settings = $.extend({}, $.DynamicList.Defaults, options);
			plugin.$element.find(".DynamicList-RemoveItemButton").click(DeleteItemClicked);
			plugin.$element.find(".DynamicList-AddItemButton").click(AddItemClicked);
		}

	}

	$.DynamicList.Defaults = {
		AddItemUrl: "DynamicListItem/NewItem?item_type_fullname=__item_type_fullname__&item_index=__item_index__&model_prefix=__model_prefix__",
		OnBeforeItemDelete: function () { return true; },
		OnBeforeItemAdd: function () { return true; }
	};

	$.fn.DynamicList = function (item_typename, model_prefix, options) {

		return this.each(function () {
			if (undefined == $(this).data('DynamicList')) {
				var plugin = new $.DynamicList(this, item_typename, model_prefix, options);
				$(this).data('DynamicList', plugin);
			}
		});

	}

})(jQuery);