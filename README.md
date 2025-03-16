<h1>Aplikację konsolowa, która umożliwia procesowanie zamówienia.</h1>
<h2>Zamówienie może posiadać jeden z 6 statusów:</h2>
<ol>
  <li>Nowe</li>
  <li>W magazynie</li>
  <li>W wysyłce</li>
  <li>Zwrócono do klienta</li>
  <li>Błąd</li>
  <li>Zamknięte</li>
</ol>
<h2>Aplikacja powinna umożliwiać 5 operacji:</h2>
<ol>
  <li>Utworzenie przykładowego zamówienia</li>
  <li>Przekazanie zamówienia do magazynu</li>
  <li>Przekazanie zamówienia do wysyłki</li>
  <li>Przegląd zamówień</li>
  <li>Wyjście</li>
</ol>
<h2>Zamówienie składa się co najmniej z właściwości:</h2>
<ul>
  <li>kwota zamówienia</li>
  <li>nazwa produktu</li>
  <li>typ klienta (firma, osoba fizyczna)</li>
  <li>adres dostawy</li>
  <li>sposób płatności (karta, gotówka przy odbiorze)</li>
</ul>
<h2>Warunki biznesowe:</h2>
<ul>
  <li>Zamówienia za nie mniej niż 2500 z płatnością gotówką przy odbiorze powinny zostać
zwrócone do klienta przy próbie przekazania do magazynu.</li>
  <li>Zamówienia przekazane do wysyłki powinny po co najwyżej 5 sekundach zmienić status na
„wysłane”.</li>
  <li>Zamówienia bez adresu wysyłki powinny kończyć się błędem.</li>
</ul>

