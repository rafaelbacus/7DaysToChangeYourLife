//Sass configuration
var gulp = require('gulp');
var sass = require('gulp-sass');
var autoprefixer = require('gulp-autoprefixer');
var uglify = require('gulp-uglify');
var rename = require('gulp-rename');
var clean = require('gulp-rimraf');
var util = require('gulp-util');
var cssmin = require('gulp-cssmin');

var cssInput = 'Styles/*.scss';
var cssOutput = 'wwwroot/css/';
var sassOptions = {style: 'expanded'};
var autoprefixerOptions = {browsers: ['last 10 versions']};
var renameOptions = {suffix: '.min'};

gulp.task('css', function(){
    gulp.src(cssOutput + "*", { read : false})
        .pipe(clean());

    gulp.src(cssInput)
        .pipe(sass(sassOptions))
        .pipe(autoprefixer(autoprefixerOptions))
        .pipe(gulp.dest(cssOutput))
        .pipe(cssmin({level: 2}))
        .pipe(rename(renameOptions))
        .pipe(gulp.dest(cssOutput));
});

var jsInput = 'Scripts/*.js';
var jsOutput = 'wwwroot/js/';

gulp.task('js', function(){
    gulp.src(jsOutput + "*", { read : false})
        .pipe(clean());

    gulp.src(jsInput)
        .pipe(gulp.dest(jsOutput))
        .pipe(uglify())
        .pipe(rename(renameOptions))
        .pipe(gulp.dest(jsOutput));
});