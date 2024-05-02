const path = require('path');
const CSSExtractPlugin = require('mini-css-extract-plugin')

let isProduction

module.exports = (env, argv) => {
    isProduction = true
    return {
        mode: process.env.NODE_ENV ? "production" : "development",
        entry: {
            app: './src/index.js',
        },
        optimization: {
            minimize: isProduction
        },
        output: {
            filename: "[name]-bundle.js",
            path: path.resolve("../resources/js")

        },
        devtool: isProduction ? false : "source-map",
        module: {
            rules: [{
                test: /\.(js|jsx)$/,
                exclude: /(node_modules|bower_components)/,
                loader: 'babel-loader',
                options: { presets: ['@babel/env', '@babel/preset-react'] },
            },
            {
                test: /\.css$/i,
                use: [!isProduction ? 'style-loader' : CSSExtractPlugin.loader, 'css-loader', 'postcss-loader']

            },
            {
                test: /\.(png|svg|jpg|jpeg|gif)$/i,
                type: 'asset/resource',
            },
            {
                test: /\.(woff|woff2|eot|ttf|otf)$/i,
                type: 'asset/resource',
            },
            {
                test: /\.json$/,
                loader: 'json-loader'
            }
            ]
        },
        plugins: [
            new CSSExtractPlugin({
                filename: '[name]-bundle.css',
            }),

        ],
        watch: process.env.NODE_ENV !== 'production' && true,
    };
}
