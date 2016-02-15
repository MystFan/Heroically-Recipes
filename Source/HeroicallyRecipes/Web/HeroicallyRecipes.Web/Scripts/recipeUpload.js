(function () {
    'use strict';

    var $ingradientsContainer = $('#add-ingradients-container');
    var $btnAddIngredient = $('#btn-add-ingredient');
    var $recipeImagesContainer = $('#add-images-container');
    var $btnAddRecipeImage = $('#btn-add-image');

    $btnAddIngredient.on('click', function (ev) {
        var ingredientCount = $ingradientsContainer.children('textarea').length;
        var $textarea = $('<textarea/>').attr({
            value: 'Ingradient',
            name: 'Ingredients[' + ingredientCount + ']',
            cols: 50,
            'class': 'form-control'
        });

        $ingradientsContainer
            .append('<br/>')
            .append($textarea);
    });

    $btnAddRecipeImage.on('click', function (ev) {
        var recipeImagesCount = $recipeImagesContainer.children('input').length;
        var $inputFile = $('<input/>').attr({
            name: 'recipeImages',
            type: 'file',
            'class': 'form-control'
        });

        $recipeImagesContainer
            .append('<br/>')
            .append($inputFile);
    });

})();