


var contextMenu = Windows.UI.Popups.PopupMenu();

contextMenu.commands.append(
    new Windows.UI.Popups.UICommand("Clark Sell", somethingHandler));

contextMenu.commands.append(
    new Windows.UI.Popups.UICommandSeparator());

contextMenu.commands.append(
    new Windows.UI.Popups.UICommand("Jeff Blankenburg", somethingHandler));

contextMenu.commands.append(
    new Windows.UI.Popups.UICommand("31 Days of Windows 8", somethingHandler));

contextMenu.commands.append(
    new Windows.UI.Popups.UICommand("Edit", somethingHandler));

contextMenu.commands.append(
    new Windows.UI.Popups.UICommand("Delete", somethingHandler));


document.getElementById("myImage").addEventListener("contextmenu",
    imageContentHandler, false);