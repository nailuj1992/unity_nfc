# Unity NFC Testing
This module is done with the idea of implementing the passport scanning with NFC technology on Unity, and for Android devices only.

## 1. Previous notions

### AAR files

First of all, I included the next .AAR files in the directory *\Assets\Plugins\Android*. These files are in charge of carrying out the basic logic of scanning the passport chip, and they are written in Java (in fact, they should be Android library plugins, so Unity can recognize and execute them properly).

![Screenshot 2024-07-03 145410](https://github.com/nailuj1992/unity_nfc/assets/14367140/56007b39-686d-4adb-ba6c-ed06498cdee5)

From these 6 files, there is one of them which was lightly modified for this project, and another that was created from scratch.

#### File *jmrtd-0.7.40.aar*

The modified file is “*jmrtd-0.7.40.aar*”, and the only change that was made and compiled is located in **PassportService** class -a void method was added because C# has a little problem with methods that have something to return-.

![Screenshot 2024-07-03 150406](https://github.com/nailuj1992/unity_nfc/assets/14367140/d1ca3016-1724-43b4-888f-01151dda6016)

The repository where the source code of this file is located over here: https://github.com/nailuj1992/JMRTD-Unity

#### File *ImagesDecoder.aar*

On the other side, the new file is named “*ImagesDecoder.aar*”, and it is a little project that contains some code to read and process the image that is obtained as an encoded string from the passport chip. That mini-project contains the next three classes: **ImagesDecoder**, **ImageJpeg2000Info**, and **Image**.

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/ed645517-aca8-4b55-9e64-ccfab87761e0)

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/01fb861b-0b07-4106-8253-8414d26cc41d)

![Screenshot 2024-07-03 151026](https://github.com/nailuj1992/unity_nfc/assets/14367140/6d10e41e-2b22-4712-8fa3-8c2551536263)

The repository where the source code of this file is located over here: https://github.com/nailuj1992/ImagesDecoder-Unity

#### File *NativeNFC.aar*

Finally, the second modified file is “*NativeNFC.aar*”, and the only change that was made and compiled is located in **NativeNFC** class -the methods *getLastTag()* and *getLastIntent()* were added because in the original file, these methods do not exist, and the method *getLastTag()* is especially needed for the proper functioning of this NFC scan module-.

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/daf91c34-f485-4374-8dec-8d1db35f06d4)

The repository where the source code of this file is located over here: https://github.com/nailuj1992/NativeNFC-Unity

## 2. NFC Extraction

There is a screen that is responsible for listening if a NFC tag is read or identified.

![Screenshot 2024-07-03 152507](https://github.com/nailuj1992/unity_nfc/assets/14367140/8f74eba1-a431-4827-ab1b-91af26eb1489)

When a tag is behind the mobile device, the method “*UpdateTagInfo()*” will be executed.

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/3d8b8c5f-382b-412d-9673-7ed2917cd8f7)

The content for that method has no importance for us… except the last line. The method “*ReadTagGetInfo()*” seeks the MRZ information found in the camera phase, then it is passed as a parameter to a new object called **ScanPassport**, which is intended to work as the core of the NFC extraction. If the passport information is finally gotten, the screen shall be redirected to the next screen. Now, let’s deep dive into the **ScanPassport.GetPassportInfo()** method.

### ScanPassport.GetPassportInfo() method

The first thing that happens when this method is called is that a JSON object is obtained from the MRZ string information. That JSON object contains the next fields:

![Screenshot 2024-07-03 154037](https://github.com/nailuj1992/unity_nfc/assets/14367140/2e9ec68d-ce0e-49b4-a886-43c6fdc352a0)

From those fields, we only need the Passport number (field ‘*pnumber*’), Date of birth (field ‘*dateBirth*’), and Expiration date (field ‘*expirationDate*’).

Going forward, if the identified tag corresponds with ISODep technology, we get the variables from the environment, and create a new object from **IsoDepUtils** class.

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/2fd656c5-af57-4728-ad67-540b4d492d63)

After that step happens, we get the three fields from the JSON object that was mentioned before, and we start the BAC process. 

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/fffbab6e-0956-4082-8905-19a909e305cf)

When we call the method **IsoDepUtils.InitializeService()**, we are saying to the mobile device that we require some resources to begin the NFC scan.

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/6c781893-b409-4159-8a50-14519ec08c35)

By the way, the method **IsoDepUtils.DoBACProcess()** is intended to execute the BAC process. BAC means Basic Access Control, and it is intended to avoid the data stored in the chip can be electronically read without authorizing this reading of the document (skimming), and the unencrypted communication between a chip and a reader can be eavesdropped within a distance of several meters. So, BAC is based purely on symmetric cryptography to store the information within.

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/0d51f6ba-c6cd-418a-ab0f-23ca5287d7a1)

Let’s note that we are calling the method “*doBACNow*”, which is the class that I added to the *JMRTD AAR* file (see **section 1.** for more information). On the other hand, when we invoke the method **sendSelectApplet(false)**, we are telling the chip to be ready because a BAC process will begin.

Now, we are ready to request the information contained into the passport chip.

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/e6c415b1-0bfb-461b-a18a-2974a34e2089)

### Method IsoDepUtils.GetPersonalDetails() for File DG1

If we want to get and decrypt the information within the file DG1 on the passport chip, we should take the next steps.

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/7e17f1a1-ce22-4deb-ad30-689c4f98a5fa)

The constant **EF_DG1** is declared as short and its value is *0x0101*. This file contains information like personal number, nationality and document code. With the variable mrzInfo, we can access to that data with these lines:

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/c0930cec-6b75-475b-bd4c-0eaa80c7c2d1)

### Method IsoDepUtils.GetFaceImage() for File DG2

The constant **EF_DG2** is declared as short and its value is *0x0102*. This file simply contains a Base64 string representation of the face image. To extract that image, we should do the next steps.

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/f274ac28-ea19-4d25-b7c0-e02a06cbee07)

It is too much text, but the fact is that we need to get the face image info contained into the chip. The main focus is in the highlighted line, which corresponds to a method in the library “*ImagesDecoder.aar*” that was created from scratch (see **section 1.** for more information). Doing so, an Image object (Java object, in fact) is obtained, so we can get the Base64 string representation from that object.

### Method IsoDepUtils.GetAdditionalDetails() for File DG11

The constant **EF_DG11** is declared as short and its value is *0x010B*. This file is similar to file DG1, but it *might* contain different information (big emphasis on the word *MIGHT*). If this file has information on it, we simply execute the next lines of code.

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/d58f18cc-d0af-4987-a202-06d5e4cbb04f)

The information that might be included is the name of the holder, full date of birth, personal number, and telephone, among others.

### Method IsoDepUtils.CloseService() to finish connections

Finally, but not less important, we should close the connections that we established between the mobile device and the passport chip, in order to release resources for the device.

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/96c137c8-b11e-4602-958e-49a949cb0a91)

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/7efcb17d-5eb5-491e-81fc-a38eec7eba99)

### JSON final representation

When all the data is gathered from the passport chip, it is temporarily stored into a JSON object. The structure of that JSON object is the next one.

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/b0ef6cff-4bea-4ac1-9598-04cfea83280d)

If everything is successful, this final screen shall appear.

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/f0613d95-68ca-4817-8783-8b5a55e9271f)

When we click on the right button, the relevant information is stored in the Player prefs.

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/9657c023-7009-47d8-af31-8197e2783d6c)

Finally, the basic information is shown in the next screen (face, date, and passport number were censored).

![image](https://github.com/nailuj1992/unity_nfc/assets/14367140/acfeb479-9b4f-4924-a5be-a516b88ba30f)

# Bibliography

https://www.icao.int/publications/pages/publication.aspx?docnum=9303
https://www.icao.int/publications/Documents/9303_p11_cons_en.pdf
https://github.com/alimertozdemir/EPassportNFCReader/tree/master?tab=readme-ov-file
https://jmrtd.org/
