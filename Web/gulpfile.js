//Sass configuration
var gulp = require('gulp');
var sass = require('gulp-sass');
var autoprefixer = require('gulp-autoprefixer');
var minify = require('gulp-minify');
var rename = require('gulp-rename');

var input = 'Styles/*.scss';
var output = 'wwwroot/css/';
var sassOptions = {style: 'expanded'};
var autoprefixerOptions = {browsers: ['last 10 versions']};
var renameOptions = {suffix: '.min'};

gulp.task('css', function(){
    gulp.src(input)
        .pipe(sass(sassOptions))
        .pipe(autoprefixer(autoprefixerOptions))
        .pipe(gulp.dest(output))
        .pipe(minify())
        .pipe(rename(renameOptions))
        .pipe(gulp.dest(output))
});