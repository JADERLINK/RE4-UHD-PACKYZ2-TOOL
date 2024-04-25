# RE4-UHD-PACKYZ2-TOOL
Extract and repack RE4 UHD .pack/.pack.yz2 files

Translate from Portuguese Brazil

Programa destinado a extrair e reempacotar arquivos .pack/pack.yz2
<br> Ao extrair será gerado um arquivo de extenção .idxpack, ele será usado para o repack.

**update: 1.0.1**
<br>Agora, além dos arquivos .dds e .tga, ele aceita arquivos .empty que representa que aquela numeração está vazia, assim pode você pular a numeração sem ocupar mais espaço no arquivo.
<br>Nota: você não pode fazer referência a numerações de arquivos "empty" no tpl, pois realmente não existe imagem ali. No lugar, será exibida a textura de botões.

## Extract

Exemplo:
<br>JADERLINK_PACKYZ2_TOOL.exe "01000000.pack.yz2"

* Ira gerar um arquivo de nome "01000000.pack.yz2.idxpack"
* Ira criar uma pasta de nome "01000000"
* Na pasta vão conter as texturas, nomeadas numericamente com 4 dígitos. Ex: 0000.dds

## Repack

Exemplo:
<br>JADERLINK_PACKYZ2_TOOL.exe "01000000.pack.yz2.idxpack"

* Vai ler as imagens da pasta "01000000"
A quantidade é definida pela numeração, (então não deixe imagens faltando no meio).
* O nome do arquivo gerado é o mesmo nome do idxpack, mas sem o .idxpack;

**At.te: JADERLINK**
<br>2024-04-25