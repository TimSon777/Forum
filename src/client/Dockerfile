FROM node:12-alpine as build
WORKDIR /app
COPY package.json /app/package.json
RUN npm install --only=prod
COPY . /app

RUN npm run build
FROM nginx:1.16.0-alpine
COPY --from=build /app/build /usr/share/nginx/html

EXPOSE 80
EXPOSE 443
CMD ["nginx", "-g", "daemon off;"]