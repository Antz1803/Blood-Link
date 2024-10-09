$(function () {
    $('#link-wallet').click(async function () {
        if (window.solana && window.solana.isPhantom) {
            try {
                // Connect to the Phantom wallet and get the public key
                const response = await window.solana.connect();
                const publicKey = response.publicKey.toString();
                console.log('Phantom wallet public key:', publicKey);

                // Send the public key to the server to link with the user's account
                if ($(this).text() == "Remove Wallet") {
                    $.ajax({
                        url: '/Wallet/RemovePhantomWallet', // Server-side endpoint for linking
                        method: 'POST',
                        success: function (response) {
                            if (response.success) {
                                alert('Phantom successfully removed!');
                                $.ajax({
                                    url: '/Login/LogOut', // URL of the logout action
                                    type: 'POST', // Use POST to log out
                                });
                            } else {
                                alert('Error removing Phantom wallet. Please try again.');
                            }
                        },
                        error: function (err) {
                            console.error('Error linking Phantom wallet:', err);
                        }
                    });
                }
                else {
                    $.ajax({
                        url: '/Wallet/LinkPhantomWallet', // Server-side endpoint for linking
                        method: 'POST',
                        data: { walletPublicKey: publicKey },
                        success: function (response) {
                            if (response.success) {
                                alert('Phantom wallet successfully linked!');
                                $.ajax({
                                    url: '/Login/LogOut', // URL of the logout action
                                    type: 'POST', // Use POST to log out
                                });

                            } else {
                                alert('Error linking Phantom wallet. Please try again.');
                            }
                        },
                        error: function (err) {
                            console.error('Error linking Phantom wallet:', err);
                        }
                    });
                }

            } catch (err) {
                console.error('Error connecting to Phantom wallet:', err);
            }
        } else {
            alert('Phantom wallet not found. Please install the Phantom wallet extension.');
            window.open('https://phantom.app/', '_blank');
        }
    });
});
