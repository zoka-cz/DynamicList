/*
 * Script for Zoka.AspNetCore.Components.DynamicList component.
 * Once the editor for DynamicList is rendered, the component is initialized (see Usage) and it handles the addition
 * and removing of items in the list.
 *
 * Usage:
 *		$("#DynamicListWrapperId").DynamicList("AssemblyQualifiedItemModelType", "ModelPrefix", { options_object })
 *
 * AssemblyQualifiedItemModelType - the string containing the fully assembly qualified name of the list item type as
 *		returned by Type.AssemblyQualifiedName property
 * ModelPrefix - the prefix to be used as the prefix for editor input names.
 * options_object - object containing the settings of the DynamicList behaviour
 *		It may contain following items:
 *			AddItemUrl - The url, which is called when the new item is about to be added to the DynamicList editor (add button clicked)
 *				This url should return html with single item editor
 *			OnBeforeItemDelete - The function which returns boolean type - when true, the Item is then deleted from DynamicList
 *				If it returns false, the item is not deleted
 *			OnBeforeItemAdd - The function which returns boolean type - when true, the Item is not added to the DynamicList
 *				If it returns tru, the item is added
 *
 * Remarks:
 *		You may change the behaviour of the DynamicList component "globally" (for the page) by changing the Defaults, e.g.:
 *			$.DynamicList.Defaults.OnBeforeItemDelete = function () {
 *				alert("Are you sure you want to delete the item?");
 *			}
 *		placed anywhere on the page will change the behaviour for all DynamicList on the page (unless changed by component initialization)
 *		so it asks before the item is deleted from the list.
 *
 */


(function ($) {

	$.DynamicList = function (element, item_typename, model_prefix, options) {

		// fired, when the RemoveButton is clicked
		var DeleteItemClicked = function (e) {
			if (plugin.settings.OnBeforeItemDelete()) {
				// delete item row only in case, the OnBeforeItemDelete callback allows it
				$(this).closest(".DynamicList-Row").remove();
			}
		}

		// fired, when the AddButton is clicked
		var AddItemClicked = function (e) {
			// add row only in case, the OnBeforeItemAdd callback allows it
			if (!plugin.settings.OnBeforeItemAdd())
				return;

			// find highest index, add 1 and use it is new item index
			var indexes = plugin.$element.find("input[name=\"" + plugin.model_prefix + (plugin.model_prefix.length > 0 ? "." : "") + "Index\"]").map(function () { return $(this).val(); }).get() || [];
			var item_index = 0;
			if (indexes.length != 0)
				item_index = Math.max.apply(Math, indexes) + 1;

			// load new single item editor
			$.get(
				// adapt URI by values of this instance
				encodeURI(plugin.settings.AddItemUrl
					.replace("__item_type_fullname__", plugin.item_typename)
					.replace("__item_index__", item_index)
					.replace("__model_prefix__", plugin.model_prefix)),
				null, // no data
				function (data) {
					// append editor after all editors
					plugin.$element.find(".DynamicList-Rows").append(data);
					// enable validation for newly added editor
					plugin.$element.closest("form").removeData("validator");
					plugin.$element.closest("form").removeData("unobtrusiveValidation");
					$.validator.unobtrusive.parse(plugin.$element.closest("form"));
					// bind newly added RemoveButton to the click event
					plugin.$element.find(".DynamicList-RemoveItemButton").last().click(DeleteItemClicked);
				}
			);
		}

		// store the needed data
		var plugin = this;
		plugin.settings = {}
		plugin.item_typename = item_typename;
		plugin.model_prefix = model_prefix;
		plugin.$element = $(element);

		{
			// store instance settings
			plugin.settings = $.extend({}, $.DynamicList.Defaults, options);
			// bind buttons to click event
			plugin.$element.find(".DynamicList-RemoveItemButton").click(DeleteItemClicked);
			plugin.$element.find(".DynamicList-AddItemButton").click(AddItemClicked);
		}

	}

	// default (static) settings, may be overriden by the option parameter during initialization
	$.DynamicList.Defaults = {
		AddItemUrl: "DynamicListItem/NewItem?item_type_fullname=__item_type_fullname__&item_index=__item_index__&model_prefix=__model_prefix__",
		OnBeforeItemDelete: function () { return true; },
		OnBeforeItemAdd: function () { return true; }
	};

	// bind DynamicList to the jQuery
	$.fn.DynamicList = function (item_typename, model_prefix, options) {

		return this.each(function () {
			if (undefined == $(this).data('DynamicList')) {
				var plugin = new $.DynamicList(this, item_typename, model_prefix, options);
				$(this).data('DynamicList', plugin);
			}
		});

	}

})(jQuery);