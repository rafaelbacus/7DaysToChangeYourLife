//Sass configuration
var gulp = require('gulp');
var sass = require('gulp-sass');
var autoprefixer = require('gulp-autoprefixer');
var uglify = require('gulp-uglify');
var babel = require('gulp-babel');
var rename = require('gulp-rename');
var clean = require('gulp-rimraf');
var ignore = require('gulp-ignore');
var util = require('gulp-util');
var cssmin = require('gulp-cssmin');

var cssInput = 'Styles/*.scss';
var cssOutput = 'wwwroot/css/';
var sassOptions = {outputStyle: 'expanded'};
var autoprefixerOptions = {browsers: ['last 5 versions']};
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
    gulp.src(jsOutput + "*.js", { read : false})
        .pipe(clean());

    return gulp.src(jsInput)
               .pipe(gulp.dest(jsOutput))
               .pipe(babel({
                   presets: ['env']
               }))
               .on('error', function (err) { util.log(util.colors.red('[Error]'), err.toString()); })
               .pipe(uglify())
               .pipe(rename(renameOptions))
               .pipe(gulp.dest(jsOutput));
});

gulp.task('scripts',['css', 'js'], function(){ return; });

//npm config set strict-ssl true