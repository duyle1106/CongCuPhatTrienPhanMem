/**
 * @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.enterMode = CKEDITOR.ENTER_BR // pressing the ENTER Key puts the <br/> tag
    config.shiftEnterMode = CKEDITOR.ENTER_P; //pressing the SHIFT + ENTER Keys puts the <p> tag
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = 'vi';
    config.filebrowserBrowseUrl = '/assets/admin/js/plugins/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/assets/admin/js/plugins/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/assets/admin/js/plugins/ckfinder.html?Type=Flash';
    config.fileUploadUrl = '/assets/admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/HinhAnh';
    config.filebrowserFlashUploadUrl = '/assets/admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    
    CKFinder.setupCKEditor(null, '/assets/admin/js/plugins/ckfinder/');
};

