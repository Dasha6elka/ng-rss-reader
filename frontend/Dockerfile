FROM node:14-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY src ./src/
COPY tsconfig*.json ./
COPY angular.json ./
COPY .browserslistrc ./
RUN npm run build:prod

FROM nginx:1.18-alpine AS serve
WORKDIR /var/www/app
COPY --from=build /app/dist/frontend ./
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["nginx", "-g", "daemon off;"]
