var gulp = require('gulp'),
    concat = require('gulp-concat'),
    cssmin = require('gulp-cssmin'),
    uglify = require('gulp-uglify'),
    del = require('del'),
    imagemin = require('gulp-imagemin');

var webrootfolder = "wwwroot";
var paths = { webroot: "./" + webrootfolder + "/" };
paths.js = paths.webroot + "js/**/*.js";
paths.css = paths.webroot + "css/**/*.css";
paths.myLib = paths.webroot + "myLib";
paths.image = paths.webroot + "img/**/*.*";

gulp.task("clean", function () {
    return del.sync(paths.myLib + "/");
});

gulp.task("minjs", function () {
    return gulp.src(paths.js)
        .pipe(concat("all.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest(paths.myLib));
});

gulp.task("mincss", function () {
    return gulp.src(paths.css)
        .pipe(concat("style.css"))
        .pipe(cssmin())
        .pipe(gulp.dest(paths.myLib));
});

gulp.task("minimage", function () {
    return gulp.src(paths.image)
        .pipe(imagemin())
        .pipe(gulp.dest(paths.myLib));
});
