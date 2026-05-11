# Playwright UI Tests

Автоматизированные UI-тесты для сайта [automationexercise.com](https://automationexercise.com) на C#.

## Структура проекта

Решение состоит из трёх проектов:

- **Core** — инфраструктура (`PlaywrightDriver`, `ConfigurationProvider`, `TestSettings`)
- **Pages** — Page Object Model (`HomePage`, `LoginPage`, `SignUpPage`, `AccountPage`, `ContactUsPage`, `TestCasesPage`, `ProductsPage`, `ProductDetailsPage`, `CartPage`, `CartModalDialog`, `CheckoutPage`, `PaymentPage`)
- **UITests** — тесты, билдеры, хелперы и модели

## Конфигурация

Настройки в `Core/appsettings.json`:

| Параметр | Описание | Пример |
|----------|----------|--------|
| BaseUrl | Базовый URL сайта | https://automationexercise.com |
| Browser | Firefox / Chrome / Webkit | Firefox |
| Headless | Запуск без UI | false |
| SlowMo | Задержка между действиями (мс) | 0 |
| DefaultTimeout | Таймаут по умолчанию (мс) | 30000 |

## Покрытые тест-кейсы

Тесты основаны на кейсах с [automationexercise.com/test_cases](https://automationexercise.com/test_cases).
![img.png](img.png)
### Test Case 1: Регистрация нового пользователя
**Файл:** `RegisterTests.cs` → `Register_NewUserShouldBeRegistered`

1. Открыть главную страницу, проверить её загрузку
2. Перейти в Signup / Login
3. Ввести имя и email, нажать Signup
4. Убедиться, что открылась форма "Enter Account Information"
5. Заполнить личные и адресные данные
6. Создать аккаунт и проверить сообщение "Account Created!"
7. Убедиться, что отображается "Logged in as username"

### Test Case 2: Вход с корректными данными
**Файл:** `LoginTests.cs` → `Login_WithCorrectCredentials_UserIsLoggedIn`

1. Открыть главную страницу
2. Перейти в Signup / Login
3. Ввести корректные email и пароль
4. Убедиться, что отображается "Logged in as username"

### Test Case 3: Вход с некорректными данными
**Файл:** `LoginTests.cs` → `Login_WithIncorrectCredentials_ErrorIsShown`

1. Открыть главную страницу
2. Перейти в Signup / Login
3. Ввести неверные email и пароль
4. Убедиться, что отображается ошибка "Your email or password is incorrect!"

### Test Case 4: Выход из аккаунта
**Файл:** `LoginTests.cs` → `Logout_LoggedInUser_RedirectedToLoginPage`

1. Открыть главную страницу и войти под корректными данными
2. Убедиться, что отображается "Logged in as username"
3. Нажать Logout
4. Проверить переход на страницу логина

### Test Case 5: Регистрация с уже существующим email
**Файл:** `RegisterWithExistingEmailTests.cs` → `Register_WithExistingEmail_ErrorIsShown`

1. Предварительно зарегистрировать пользователя (в SetUp)
2. Открыть главную страницу
3. Перейти в Signup / Login
4. Ввести имя и уже существующий email
5. Убедиться, что отображается ошибка "Email Address already exist!"

### Test Case 6: Отправка формы Contact Us
**Файл:** `ContactUsTests.cs` → `ContactUs_FormIsSubmittedSuccessfully`

1. Открыть главную страницу
2. Перейти в раздел "Contact us"
3. Убедиться, что отображается заголовок "GET IN TOUCH"
4. Заполнить поля Name, Email, Subject, Message
5. Прикрепить файл к форме
6. Нажать Submit и принять браузерный alert
7. Проверить сообщение "Success! Your details have been submitted successfully."
8. Вернуться на главную страницу

### Test Case 7: Переход на страницу Test Cases
**Файл:** `TestCasesPageTests.cs` → `TestCases_PageIsOpenedSuccessfully`

1. Открыть главную страницу
2. Кликнуть по ссылке "Test Cases" в шапке
3. Убедиться, что заголовок страницы "TEST CASES" отображается

### Test Case 8: Просмотр всех товаров и страницы деталей товара
**Файл:** `ProductsTests.cs` → `AllProducts_AndProductDetailPage_AreDisplayedCorrectly`

1. Открыть главную страницу
2. Перейти в раздел "Products"
3. Убедиться, что отображается заголовок "ALL PRODUCTS" и список товаров
4. Кликнуть "View Product" у первого товара
5. Проверить отображение названия, категории, цены, поля Availability, Condition, Brand

### Test Case 9: Поиск товара
**Файл:** `ProductsTests.cs` → `SearchProduct_ReturnsRelevantResults`

1. Открыть главную страницу
2. Перейти в раздел "Products"
3. Ввести поисковую фразу и нажать кнопку поиска
4. Убедиться в отображении заголовка "SEARCHED PRODUCTS"
5. Проверить, что результаты поиска не пусты

### Test Case 10: Подписка на рассылку с главной страницы
**Файл:** `SubscriptionTests.cs` → `Subscription_OnHomePage_IsSuccessful`

1. Открыть главную страницу
2. Прокрутить страницу к футеру
3. Убедиться в отображении заголовка "SUBSCRIPTION"
4. Ввести email и нажать кнопку подписки
5. Проверить сообщение "You have been successfully subscribed!"

### Test Case 11: Подписка на рассылку со страницы корзины
**Файл:** `SubscriptionInCartTests.cs` → `Subscription_OnCartPage_IsSuccessful`

1. Открыть главную страницу
2. Перейти на страницу корзины
3. Прокрутить страницу к футеру
4. Убедиться в отображении заголовка "SUBSCRIPTION"
5. Ввести email и нажать кнопку подписки
6. Проверить сообщение об успешной подписке

### Test Case 12: Добавление товаров в корзину
**Файл:** `CartTests.cs` → `AddProducts_ToCart_AreVisibleInCart`

1. Открыть главную страницу
2. Перейти в раздел Products
3. Навести курсор на первый товар и нажать "Add to cart"
4. В модальном окне нажать "Continue Shopping"
5. Навести курсор на второй товар и нажать "Add to cart"
6. В модальном окне нажать "View Cart"
7. Убедиться, что в корзине отображаются 2 добавленных товара

### Test Case 13: Проверка количества товара в корзине
**Файл:** `CartTests.cs` → `ProductQuantity_InCart_MatchesSelectedQuantity`

1. Открыть главную страницу
2. Перейти в раздел Products → открыть страницу деталей первого товара
3. Установить количество (Quantity) = 4
4. Нажать "Add to cart"
5. В модальном окне нажать "View Cart"
6. Убедиться, что в корзине количество товара совпадает с заданным

### Test Case 14: Оформление заказа — регистрация во время чекаута
**Файл:** `PlaceOrderRegisterWhileCheckoutTests.cs` → `PlaceOrder_RegisterWhileCheckout_OrderIsConfirmed`

1. Добавить товар в корзину
2. Перейти в корзину и нажать "Proceed To Checkout"
3. В модалке нажать "Register / Login"
4. Заполнить форму регистрации и создать аккаунт
5. Убедиться в авторизации и вернуться к оформлению заказа
6. Заполнить комментарий и нажать "Place Order"
7. Ввести платёжные реквизиты и подтвердить заказ
8. Проверить сообщение об успешном оформлении заказа

### Test Case 15: Оформление заказа — регистрация до чекаута
**Файл:** `PlaceOrderRegisterBeforeCheckoutTests.cs` → `PlaceOrder_RegisterBeforeCheckout_OrderIsConfirmed`

1. Зарегистрировать пользователя (в SetUp) и войти под ним
2. Добавить товар в корзину
3. Перейти в корзину и нажать "Proceed To Checkout"
4. Убедиться в отображении адресов доставки и биллинга
5. Заполнить комментарий и нажать "Place Order"
6. Ввести платёжные реквизиты и подтвердить заказ
7. Проверить сообщение об успешном оформлении заказа

## Архитектура

- **Page Object Model** — каждой странице сайта соответствует свой класс в `Pages/`
- **Builder pattern** — `UserBuilder` и `LoginDataBuilder` для подготовки тестовых данных
- **AuthHelper** — инкапсулирует регистрацию и удаление аккаунта (используется в SetUp/TearDown)
- **BaseTest** — абстрактный базовый класс с настройкой Playwright и хуками `OnSetUpAsync`/`OnTearDownAsync`
- **Уникальный email** через `Guid.NewGuid()` в `UserBuilder` обеспечивает изоляцию тестов
