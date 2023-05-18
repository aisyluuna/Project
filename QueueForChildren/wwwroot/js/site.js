// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var loader = function () {
	return `<div class="h-100 d-flex align-items-center justify-content-center">
				<div class="spinner-border" role="status">
				<span class="sr-only">Загрузка...</span>
				</div>
				</div>`;
}

var fetchWithUpdateContent = function (id, url, options, callbackIfNotSuccess) {
	$(id).html(loader());
	fetch(url, options)
		.then(function (data) {
			if (data.ok) {
				data.text().then(function (text) {
					$(id).html(text);
				})
			}
			else if (callbackIfNotSuccess) {
				callbackIfNotSuccess();
            }
		})
}